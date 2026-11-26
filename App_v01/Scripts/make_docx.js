#!/usr/bin/env node
/**
 * make_docx.js  <blocks-json-path> <output-docx-path>
 *
 * Reads a JSON array of { type, text } blocks and writes a Word .docx.
 * Block types: h1, h2, h3, label, para, pageBreak
 */

const path = require('path');
const fs   = require('fs');

// Always load docx from the local node_modules beside this script.
// Run `npm install` in the Scripts folder once before first use.
const docxPath = path.join(__dirname, 'node_modules', 'docx');
const { Document, Packer, Paragraph, TextRun, HeadingLevel, PageBreak,
        Table, TableRow, TableCell, WidthType, BorderStyle, ShadingType } = require(docxPath);

const [,, dataPath, outPath] = process.argv;
if (!dataPath || !outPath) {
    process.stderr.write('Usage: node make_docx.js <blocks.json> <output.docx>\n');
    process.exit(1);
}

// Normalise blocks: C# JsonSerializer uses PascalCase by default (Type/Text)
const rawBlocks = JSON.parse(fs.readFileSync(dataPath, 'utf8'));
const blocks = rawBlocks.map(b => ({
    type:  b.type  ?? b.Type,
    text:  b.text  ?? b.Text,
    label: b.label ?? b.Label,
    col1:  b.col1  ?? b.Col1,
    col2:  b.col2  ?? b.Col2,
    col3:  b.col3  ?? b.Col3,
    col4:  b.col4  ?? b.Col4,
}));

const FONT      = 'Calibri';
const FONT_H    = 'Times New Roman';
const BODY_SIZE = 22;   // half-points = 11 pt
const H1_SIZE   = 28;   // 14 pt
const H2_SIZE   = 28;   // 14 pt
const H3_SIZE   = 24;   // 12 pt
const LBL_SIZE  = 20;   // 10 pt

// ── Border helpers ────────────────────────────────────────────────────────────
const BORDER = { style: BorderStyle.SINGLE, size: 4, color: 'BBBBBB' };
const CELL_BORDERS = { top: BORDER, bottom: BORDER, left: BORDER, right: BORDER };

// Cell shading for label column
const LBL_SHADING = { type: ShadingType.CLEAR, fill: 'EEF2FA' };

function cell(text, opts = {}) {
    const { bold = false, shading, widthPct } = opts;
    const width = widthPct ? { size: widthPct, type: WidthType.PERCENTAGE } : undefined;
    // Split text on newlines so multi-line cell values render as separate paragraphs
    const lines = (text || '').split('\n').filter(l => l.trim() !== '');
    const paras = lines.length > 0
        ? lines.map(l => new Paragraph({
            children: [new TextRun({ text: l, font: FONT, size: BODY_SIZE, bold })],
            spacing: { after: 60 },
          }))
        : [new Paragraph({ children: [new TextRun({ text: '', font: FONT, size: BODY_SIZE })], spacing: { after: 60 } })];
    return new TableCell({
        children: paras,
        borders: CELL_BORDERS,
        shading,
        width,
    });
}

// 2-column label|value table row
function twoColRow(label, value) {
    return new TableRow({
        children: [
            cell(label, { bold: true, shading: LBL_SHADING, widthPct: 36 }),
            cell(value, { widthPct: 64 }),
        ],
    });
}

// 2-column equal-width header row (e.g. ICE table: Intercurrent Event | Strategy)
function twoColEqualHeader(col1, col2) {
    return new TableRow({
        children: [
            cell(col1, { bold: true, shading: LBL_SHADING, widthPct: 50 }),
            cell(col2, { bold: true, shading: LBL_SHADING, widthPct: 50 }),
        ],
        tableHeader: true,
    });
}

// 2-column equal-width data row
function twoColEqualRow(col1, col2) {
    return new TableRow({
        children: [
            cell(col1, { widthPct: 50 }),
            cell(col2, { widthPct: 50 }),
        ],
    });
}

function makeTwoColEqualTable(rows) {
    return new Table({
        width: { size: 100, type: WidthType.PERCENTAGE },
        rows,
    });
}

// 3-column table row (change log: description | rationale | section)
function threeColRow(col1, col2, col3) {
    return new TableRow({
        children: [
            cell(col1, { widthPct: 38 }),
            cell(col2, { widthPct: 32 }),
            cell(col3, { widthPct: 30 }),
        ],
    });
}

// 3-column header row (bold, shaded)
function threeColHeader(col1, col2, col3) {
    return new TableRow({
        children: [
            cell(col1, { bold: true, shading: LBL_SHADING, widthPct: 38 }),
            cell(col2, { bold: true, shading: LBL_SHADING, widthPct: 32 }),
            cell(col3, { bold: true, shading: LBL_SHADING, widthPct: 30 }),
        ],
        tableHeader: true,
    });
}

// 4-column design table row: lbl | val | lbl | val  (mirrors page dtbl)
function fourColRow(lbl1, val1, lbl2, val2) {
    return new TableRow({
        children: [
            cell(lbl1, { bold: true, shading: LBL_SHADING, widthPct: 18 }),
            cell(val1, { widthPct: 32 }),
            cell(lbl2, { bold: true, shading: LBL_SHADING, widthPct: 18 }),
            cell(val2, { widthPct: 32 }),
        ],
    });
}

// 4-column spanning row: label in col1 (shaded), value spanning cols 2-4
// Implemented as a single-column merge: label col (18%) + value col (82% via colspan)
// docx@9 TableCell supports columnSpan for column merging
function fourColSpanRow(label, value) {
    return new TableRow({
        children: [
            cell(label, { bold: true, shading: LBL_SHADING, widthPct: 18 }),
            new TableCell({
                children: (value || '').split('\n').filter(l => l.trim()).map(l =>
                    new Paragraph({ children: [new TextRun({ text: l, font: FONT, size: BODY_SIZE })], spacing: { after: 60 } })
                ).concat(
                    (value || '').split('\n').filter(l => l.trim()).length === 0
                        ? [new Paragraph({ children: [new TextRun({ text: '', font: FONT, size: BODY_SIZE })], spacing: { after: 60 } })]
                        : []
                ),
                borders: CELL_BORDERS,
                columnSpan: 3,
            }),
        ],
    });
}

function makeTwoColTable(rows) {
    return new Table({
        width: { size: 100, type: WidthType.PERCENTAGE },
        rows,
    });
}

function makeThreeColTable(rows) {
    return new Table({
        width: { size: 100, type: WidthType.PERCENTAGE },
        rows,
    });
}

function makeFourColTable(rows) {
    return new Table({
        width: { size: 100, type: WidthType.PERCENTAGE },
        rows,
    });
}

function makePara(block) {
    switch (block.type) {
        case 'h1':
            return new Paragraph({
                children: [new TextRun({ text: block.text || '', font: FONT_H, size: H1_SIZE, bold: true })],
                heading: HeadingLevel.HEADING_1,
                spacing: { before: 0, after: 240 },
            });
        case 'h2':
            return new Paragraph({
                children: [new TextRun({ text: block.text || '', font: FONT_H, size: H2_SIZE, bold: true })],
                heading: HeadingLevel.HEADING_2,
                spacing: { before: 480, after: 160 },
            });
        case 'h3':
            return new Paragraph({
                children: [new TextRun({ text: block.text || '', font: FONT_H, size: H3_SIZE, bold: true })],
                heading: HeadingLevel.HEADING_3,
                spacing: { before: 320, after: 120 },
            });
        case 'label':
            return new Paragraph({
                children: [new TextRun({ text: block.text || '', font: FONT, size: LBL_SIZE, bold: true, color: '444444' })],
                spacing: { before: 160, after: 60 },
            });
        case 'pageBreak':
            return new Paragraph({
                children: [new PageBreak()],
                spacing: { before: 0, after: 0 },
            });
        default: // para
            return new Paragraph({
                children: [new TextRun({ text: block.text || '', font: FONT, size: BODY_SIZE })],
                spacing: { after: 100 },
            });
    }
}

// ── Build top-level children, grouping consecutive table blocks into Tables ──
// tableRow     = { type:'tableRow',     label, text }                → 2-col label|value row
// tableRow2h   = { type:'tableRow2h',   col1, col2 }                → 2-col header row (equal widths)
// tableRow2    = { type:'tableRow2',    col1, col2 }                → 2-col data row (equal widths)
// tableRow3    = { type:'tableRow3',    col1, col2, col3 }           → 3-col data row
// tableRow3h   = { type:'tableRow3h',   col1, col2, col3 }          → 3-col header row
// tableRow4    = { type:'tableRow4',    label, text, col3, col4 }   → 4-col lbl|val|lbl|val row
// tableRow4span= { type:'tableRow4span',label, text }               → 4-col label + value spanning cols 2-4
//
// Consecutive same-family blocks → one Table. Any non-table block ends the current table.

function buildChildren(blocks) {
    const out = [];
    let i = 0;
    while (i < blocks.length) {
        const b = blocks[i];
        if (b.type === 'tableRow') {
            const rows = [];
            while (i < blocks.length && blocks[i].type === 'tableRow') {
                const r = blocks[i];
                rows.push(twoColRow(r.label || '', r.text || ''));
                i++;
            }
            out.push(makeTwoColTable(rows));
            // Add a small spacer paragraph after the table
            out.push(new Paragraph({ children: [], spacing: { after: 160 } }));
        } else if (b.type === 'tableRow2' || b.type === 'tableRow2h') {
            const rows = [];
            while (i < blocks.length && (blocks[i].type === 'tableRow2' || blocks[i].type === 'tableRow2h')) {
                const r = blocks[i];
                if (r.type === 'tableRow2h') {
                    rows.push(twoColEqualHeader(r.col1 || '', r.col2 || ''));
                } else {
                    rows.push(twoColEqualRow(r.col1 || '', r.col2 || ''));
                }
                i++;
            }
            out.push(makeTwoColEqualTable(rows));
            out.push(new Paragraph({ children: [], spacing: { after: 160 } }));
        } else if (b.type === 'tableRow3' || b.type === 'tableRow3h') {
            const rows = [];
            while (i < blocks.length && (blocks[i].type === 'tableRow3' || blocks[i].type === 'tableRow3h')) {
                const r = blocks[i];
                if (r.type === 'tableRow3h') {
                    rows.push(threeColHeader(r.col1 || '', r.col2 || '', r.col3 || ''));
                } else {
                    rows.push(threeColRow(r.col1 || '', r.col2 || '', r.col3 || ''));
                }
                i++;
            }
            out.push(makeThreeColTable(rows));
            out.push(new Paragraph({ children: [], spacing: { after: 160 } }));
        } else if (b.type === 'tableRow4' || b.type === 'tableRow4span') {
            const rows = [];
            while (i < blocks.length && (blocks[i].type === 'tableRow4' || blocks[i].type === 'tableRow4span')) {
                const r = blocks[i];
                if (r.type === 'tableRow4span') {
                    rows.push(fourColSpanRow(r.label || '', r.text || ''));
                } else {
                    rows.push(fourColRow(r.label || '', r.text || '', r.col3 || '', r.col4 || ''));
                }
                i++;
            }
            out.push(makeFourColTable(rows));
            out.push(new Paragraph({ children: [], spacing: { after: 160 } }));
        } else {
            out.push(makePara(b));
            i++;
        }
    }
    return out;
}

const children = buildChildren(blocks);

const doc = new Document({
    styles: {
        default: {
            document: { run: { font: FONT, size: BODY_SIZE } },
            heading1: {
                run: { font: FONT_H, size: H1_SIZE, bold: true, color: '000000' },
                paragraph: { spacing: { before: 0, after: 240 }, keepNext: true },
            },
            heading2: {
                run: { font: FONT_H, size: H2_SIZE, bold: true, color: '000000' },
                paragraph: { spacing: { before: 480, after: 160 }, keepNext: true },
            },
            heading3: {
                run: { font: FONT_H, size: H3_SIZE, bold: true, color: '000000' },
                paragraph: { spacing: { before: 320, after: 120 }, keepNext: true },
            },
        },
    },
    sections: [{
        properties: {
            page: {
                size: { width: 11906, height: 16838 },   // A4 in twips
                margin: { top: 1440, bottom: 1440, left: 1440, right: 1440 }, // 1 inch
            },
        },
        children,
    }],
});

(async () => {
    try {
        const buf = await Packer.toBuffer(doc);
        fs.writeFileSync(outPath, buf);
    } catch (err) {
        process.stderr.write(String(err) + '\n');
        process.exit(1);
    }
})();
