Instance: Title_Page
InstanceOf: m11-research-study-profile
Title: "START HERE: Exemplar Research Study with narrative"
Usage: #example
Description: """
"""
* id = "title-page-001"  // This is specified ONLY to allow documentatation of examples - it is not normally specified
* status = #active

//---------------------------------------------------------------
// Title Page
* extension[confidentialityStatement][+].valueString =  {{C181236}} //"All data is confidential"

* identifier[+].type.coding[+] = $NCIT#C132351 "Sponsor Protocol Identifier"
* identifier[=].system = $SpID
* identifier[=].value = {{C132351}} // "ABC-Exemplar"

* identifier[+].type.text = "EU CT Number"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218684
* identifier[=].system = $EMA_CTREG
* identifier[=].value = {{C218684}} //"EU-1234"

* identifier[+].type.text = "FDA IND Number"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218685
* identifier[=].system = $FDA_REG
* identifier[=].value = {{C218685}}  //"FDA-1234"

* identifier[+].type.text = "IDE Number"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218686
* identifier[=].system = $IDE_REG
* identifier[=].value = {{C218686}} //"IDE-1234"

* identifier[+].type.text = "jRCT Number"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218687
* identifier[=].system = $jRCT_REG
* identifier[=].value = {{C218687}} //"jRCT-1234"

* identifier[+].type.text = "NCT Number"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C172240
* identifier[=].system = $NCT_REG
* identifier[=].value = {{C172240}}  //"NCT-1234"

* identifier[+].type.text = "NMPA IND Number"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218688
* identifier[=].system = $NPMA_REG
* identifier[=].value = {{C218688}} //"NMPA-1234"

* identifier[+].type.text = "WHO/UTN Number"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218689
* identifier[=].system = $WHO_REG
* identifier[=].value = {{C218689}} //"WHO-1234"

* identifier[+].type.text = "Other Regulatory or Clinical Trial Identifier"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218690
* identifier[=].system = $ERA_REG
* identifier[=].value = {{C218690 }} //"ERA1234"
* identifier[=].assigner = Reference(Organization/Exemplar-Regulator-Organization)
 
* identifier[+].type.text = "Amendment Identifier"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218477
* identifier[=].system = $AmdID
* identifier[=].value = {{C218477}} "ABC-Exemplar(a)"
// for rendition if there is an amendment at this level need to go and find the amendment with this ID and display the scope

* extension[m11-research-study].extension[originalProtocol].valueCodeableConcept = {{C218672}}  //$NCIT#C21848 "Protocol Previously Amended See Summary of Changes Before the Table of Contents"

* version = {{C181232}} //"(a)"
* extension[m11-research-study].extension[versionDate].valueDate = {{C93813}} //2017-10-01

* title = {{C132346}} //"A Phase 3 Study of Inhaled Exoticillin compared to Intramuscular Exoticillin for Treatment of Bronchtis Japanese Patients with Iatrogenic Diabetes Mellitus"
* label[+].type = $NCIT#C207646 "Study Acronym"
* label[=].value = {{C94108}} //"Inhaled vs IM Exoticillin"
* label[+].type = $NCIT#C94105 "Brief Study Title"
* label[=].value = {{C94105}} //"A Phase 3 Study of Inhaled versus Intramuscular Exoticillin for Bronchtis in Diabetic Japanese Patients"


* phase = {{C48281}} //$NCIT#C15602  "Phase III Trial"
{{REPEAT * focus[+] = Reference({{InvestigationalMedicinalProduct}}) }}

* associatedParty[+].party = Reference(SponsorOrganization)
* associatedParty[=].role = $NCIT#C70793 "Clinical Study Sponsor"
* associatedParty[+].party = Reference(SponsorPractitioner)
* associatedParty[=].role = $NCIT#C70793 "Clinical Study Sponsor"

{{REPEAT
* associatedParty[+].party = Reference({{CoSponsorOrganization}})
* associatedParty[=].role = $NCIT#C215669 "Study Co-Sponsor"
* associatedParty[+].party = Reference({{CoSponsorPractitioner}})
* associatedParty[=].role = $NCIT#C215669 "Study Co-Sponsor"
}}

{{REPEAT
* associatedParty[+].party = Reference({{LocalSponsorOrganization}})
* associatedParty[=].role = $NCIT#C215670	"Local Legal Sponsor"
* associatedParty[+].party = Reference({{LocalSponsorPractitioner}})
* associatedParty[=].role = $NCIT#C215670	"Local Legal Sponsor"
}}

{{REPEAT
* associatedParty[+].party = Reference({{DevicesOrganization}})
* associatedParty[=].role = $NCIT#C156625  "Device Manufacturer"
}}

* associatedParty[+].party = Reference({{Sponsor-Expert-Practitioner}})
* associatedParty[=].role = $NCIT#C51876  "Sponsor Medical Expert"

* extension[approval].extension[approvalDate].valueDate = {{C132352}}  //2017-10-05
* extension[approval].extension[signatureUrl].valueUrl = {{C218484 }} //"https://somelocation" 
* extension[approval].extension[signature].valueSignature.data = {{C222014}}
* extension[approval].extension[signatureMethod].valueString = {{C222064}} "electronic and wet ink copy"

//---------------------------------------------------------------
// Amendment
{{REPEAT
* relatesTo[+].type = http://terminology.hl7.org/CodeSystem/artifact-relationship-type#justification
* relatesTo[=].targetReference = Reference({{Protocol-Amendment}})
}}

//---------------------------------------------------------------
// Narrative
