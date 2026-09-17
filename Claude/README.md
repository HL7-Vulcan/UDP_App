# Section 02 – Introduction (Blazor)

## Files

```
Section_02/
├── Components/
│   └── Section02.razor          ← Page component (@page "/section-02")
├── Models/
│   └── Section02Model.cs        ← Data model (6 fields)
├── Services/
│   └── DataService.cs           ← JSON / CSV / plain-text serialisation
└── wwwroot/
    └── section02-interop.js     ← JS helpers for download + file picker
```

## Setup in the host project

### 1. Register DataService (Program.cs / Startup.cs)
```csharp
builder.Services.AddScoped<UDP_App.Section_02.Services.DataService>();
```

### 2. Add the JS interop script tag
In `App.razor` or `_Host.cshtml` (before `</body>`):
```html
<script src="section02-interop.js"></script>
```

### 3. Navigate to the page
```
https://localhost:xxxx/section-02
```

## Fields

| Field name | Section | Required? |
|---|---|---|
| PurposeOfTrial | 2.1 Purpose of Trial | ✅ Required |
| RiskTrialIntervention | 2.2.1 Trial Intervention | Optional |
| RiskTrialProcedures | 2.2.1 Trial Procedures | Optional |
| RiskOther | 2.2.1 Other | Optional |
| BenefitSummary | 2.2.2 Benefit Summary | Optional |
| OverallRiskBenefit | 2.2.3 Overall Risk-Benefit Assessment | Optional |

## Behaviour

- **Grey background, green typed text** on all textarea fields
- **Red border** on PurposeOfTrial if empty when saving
- **Save to Browser** — persists to `localStorage` key `s2data`; restores on page load
- **Save to File** — downloads JSON, CSV, or Plain Text via JS
- **Load from File** — opens file picker, reads JSON, populates form
- **Clear All** — resets all fields
- **Required-fields dialog** — shown on save (browser or file) if PurposeOfTrial is empty;
  user can proceed anyway or cancel
- **Status bar** — fixed at bottom, shows save/load confirmation for 4 seconds
