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
* extension[confidentialityStatement][+].valueString =  "This document is confidential and is the property of Exemplar Pharmaceuticals Ltd. No part of it may be transmitted, reproduced, published or used without prior written authorisation from the sponsor." //"All data is confidential"

* identifier[+].type.coding[+] = $NCIT#C132351 "Sponsor Protocol Identifier"
* identifier[=].system = $SpID
* identifier[=].value = "EX2010/12" // "ABC-Exemplar"

* identifier[+].type.text = "EU CT Number"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218684
* identifier[=].system = $EMA_CTREG
* identifier[=].value = "2024-000123-42" //"EU-1234"

* identifier[+].type.text = "FDA IND Number"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218685
* identifier[=].system = $FDA_REG
* identifier[=].value = "IND-123456"  //"FDA-1234"

* identifier[+].type.text = "IDE Number"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218686
* identifier[=].system = $IDE_REG
* identifier[=].value = "IDE-1234" //"IDE-1234"

* identifier[+].type.text = "jRCT Number"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218687
* identifier[=].system = $jRCT_REG
* identifier[=].value = "jRCT2031240001" //"jRCT-1234"

* identifier[+].type.text = "NCT Number"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C172240
* identifier[=].system = $NCT_REG
* identifier[=].value = "NCT05123456"  //"NCT-1234"

* identifier[+].type.text = "NMPA IND Number"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218688
* identifier[=].system = $NPMA_REG
* identifier[=].value = "NMPA-1234" //"NMPA-1234"

* identifier[+].type.text = "WHO/UTN Number"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218689
* identifier[=].system = $WHO_REG
* identifier[=].value = "U1111-1234-5678" //"WHO-1234"

* identifier[+].type.text = "Other Regulatory or Clinical Trial Identifier"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218690
* identifier[=].system = $ERA_REG
* identifier[=].value = "ERA1234" //"ERA1234"
* identifier[=].assigner = Reference(Organization/Exemplar-Regulator-Organization)
 
* identifier[+].type.text = "Amendment Identifier"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218477
* identifier[=].system = $AmdID
* identifier[=].value = "EX2010/12(a)" //"ABC-Exemplar(a)"
// for rendition if there is an amendment at this level need to go and find the amendment with this ID and display the scope

* extension[m11-research-study].extension[originalProtocol].valueCodeableConcept = $NCIT#C49487 "No"  //$NCIT#C21848 "Protocol Previously Amended See Summary of Changes Before the Table of Contents"

* version = "(a)" //"(a)"
* extension[m11-research-study].extension[versionDate].valueDate = 2024-03-15 //2017-10-01

* title = "A Phase 3, Randomised, Double-Blind, Placebo-Controlled Study of Inhaled Exoticillin Compared to Intramuscular Exoticillin for Treatment of Bronchitis in Japanese Patients with Iatrogenic Diabetes Mellitus" //"A Phase 3 Study of Inhaled Exoticillin compared to Intramuscular Exoticillin for Treatment of Bronchtis Japanese Patients with Iatrogenic Diabetes Mellitus"
* label[+].type = $NCIT#C207646 "Study Acronym"
* label[=].value = "IIME Study" //"Inhaled vs IM Exoticillin"
* label[+].type = $NCIT#C94105 "Brief Study Title"
* label[=].value = "A Phase 3 Study of Inhaled versus Intramuscular Exoticillin for Bronchitis in Diabetic Japanese Patients" //"A Phase 3 Study of Inhaled versus Intramuscular Exoticillin for Bronchtis in Diabetic Japanese Patients"


* phase = $NCIT#C15602 "Phase 3" //$NCIT#C15602  "Phase III Trial"
* focus[+] = Reference(InvestigationalMedicinalProduct)
* associatedParty[+].party = Reference(SponsorOrganization)
* associatedParty[=].role = $NCIT#C70793 "Clinical Study Sponsor"
* associatedParty[+].party = Reference(SponsorPractitioner)
* associatedParty[=].role = $NCIT#C70793 "Clinical Study Sponsor"


* associatedParty[+].party = Reference(CoSponsorOrganization)
* associatedParty[=].role = $NCIT#C215669 "Study Co-Sponsor"
* associatedParty[+].party = Reference(CoSponsorPractitioner)
* associatedParty[=].role = $NCIT#C215669 "Study Co-Sponsor"


* associatedParty[+].party = Reference(LocalSponsorOrganization)
* associatedParty[=].role = $NCIT#C215670	"Local Legal Sponsor"
* associatedParty[+].party = Reference(LocalSponsorPractitioner)
* associatedParty[=].role = $NCIT#C215670	"Local Legal Sponsor"


* associatedParty[+].party = Reference(DevicesOrganization)
* associatedParty[=].role = $NCIT#C156625  "Device Manufacturer"

* associatedParty[+].party = Reference(Sponsor-Expert-Practitioner)
* associatedParty[=].role = $NCIT#C51876  "Sponsor Medical Expert"

* extension[approval].extension[approvalDate].valueDate = 2024-03-15  //2017-10-05
* extension[approval].extension[signatureUrl].valueUrl = "https://esign.exemplarpharma.com/EX2010-12-a" //"https://somelocation" 
* extension[approval].extension[signature].valueSignature.data = "RHIgSmFuZSBTbWl0aCwgQ2hpZWYgTWVkaWNhbCBPZmZpY2Vy"
* extension[approval].extension[signatureMethod].valueString = "Electronic signature held in the Exemplar document management system" //"electronic and wet ink copy"

//---------------------------------------------------------------
// Amendment

* relatesTo[+].type = http://terminology.hl7.org/CodeSystem/artifact-relationship-type#justification
* relatesTo[=].targetReference = Reference(Protocol-Amendment)

//---------------------------------------------------------------
// Narrative
