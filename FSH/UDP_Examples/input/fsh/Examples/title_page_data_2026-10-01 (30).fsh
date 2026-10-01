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



* associatedParty[+].party = Reference(LocalSponsorOrganization)
* associatedParty[=].role = $NCIT#C215670	"Local Legal Sponsor"
* associatedParty[+].party = Reference(LocalSponsorPractitioner)
* associatedParty[=].role = $NCIT#C215670	"Local Legal Sponsor"


* associatedParty[+].party = Reference(LocalSponsorPractitioner)
* associatedParty[=].role = $NCIT#C215670	"Local Legal Sponsor"
* associatedParty[+].party = Reference(LocalSponsorPractitioner)
* associatedParty[=].role = $NCIT#C215670	"Local Legal Sponsor"


* associatedParty[+].party = Reference(SponsorExpertPractitioner)
* associatedParty[=].role = $NCIT#C51876  "Sponsor Medical Expert"

* extension[approval].extension[approvalDate].valueDate = 2024-03-15  //2017-10-05
* extension[approval].extension[signatureUrl].valueUrl = "https://esign.exemplarpharma.com/EX2010-12-a" //"https://somelocation" 
* extension[approval].extension[signature].valueSignature.data = "RHIgSmFuZSBTbWl0aCwgQ2hpZWYgTWVkaWNhbCBPZmZpY2Vy"
* extension[approval].extension[signatureMethod].valueString = "Electronic signature held in the Exemplar document management system" //"electronic and wet ink copy"

//---------------------------------------------------------------
// Amendment

* relatesTo[+].type = http://terminology.hl7.org/CodeSystem/artifact-relationship-type#justification
* relatesTo[=].targetReference = Reference(Amendment-001)

//---------------------------------------------------------------
// Narrative
Instance: Amendment-001
InstanceOf: m11-research-study-profile
Title: "Current amendments details"
Usage: #example
Description: """
xxx
"""

* status = #active
//---------------------------------------------------------------
// Title Page
* identifier[+].type.coding[+] = $NCIT#C132351 "Sponsor Protocol Identifier"
* identifier[=].system = $SpID
* identifier[=].value = "EX2010/12" // "ABC-Exemplar"

* identifier[+].type.text = "Amendment Identifier"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218477
* identifier[=].system = $AmdID
* identifier[=].value = "EX2010/12(a)" //"ABC-Exemplar(a)"

* version = "(a)" //"(a)"
* extension[m11-research-study].extension[versionDate].valueDate = 2024-03-15 //2017-10-01

//----------------------------------

//---------------------------------------------------------------
// Title Page Amemndment Summary
//

* extension[m11-protocol-amendment][+].extension[previous].valueCodeableConcept = $NCIT#C218488	"Protocol Previously Amended See Summary of Changes Before the Table of Contents"

* extension[m11-protocol-amendment][=].extension[scope].valueCodeableConcept = $NCIT#C68846 "Global" //$NCIT#C217026	"Not Global"
//* extension[m11-protocol-amendment][=].extension[site][+].valueIdentifier.system = $AmdSite



* extension[m11-protocol-amendment][=].extension[scopeImpact][+].extension[scope].valueCodeableConcept = $NCIT#C68846 "Global" //$NCIT#C41065  "Locally"
// TODO These scopes are different
* extension[m11-protocol-amendment][=].extension[scopeImpact][+].extension[scope].valueCodeableConcept = $NCIT#C68846 "Globally" //$NCIT#C41065  "Locally"
//* extension[m11-protocol-amendment][=].extension[scopeImpact][+].extension[scope].valueCodeableConcept = $NCIT#C68846  "Global"
//* extension[m11-protocol-amendment][=].extension[scopeImpact][=].extension[number].valuePositiveInt = 983

* extension[m11-protocol-amendment][=].extension[primaryReason].valueCodeableConcept = $NCIT#C218493 "New Safety Information Available" //$NCIT#C218490  "Regulatory Agency Request to Amend Amendment Reason"
* extension[m11-protocol-amendment][=].extension[secondaryReason][+].valueCodeableConcept = $NCIT#C218494 "Manufacturing Change" //$NCIT#C218494  "Manufacturing Change Amendment Reason"
* extension[m11-protocol-amendment][=].extension[summary].valueString = "Amendment (a) introduces updated safety monitoring requirements following review of interim safety data, and clarifies the rescue medication protocol." //"Manufacturing chanage to enable packaging change to recyclable materials."

* extension[m11-protocol-amendment][=].extension[substantialImpactSafety].valueCodeableConcept = $NCIT#C49488 "Yes" // $NCIT#C49488  "Yes"
* extension[m11-protocol-amendment][=].extension[substantialImpactSafetyComment].valueString = "Updated safety monitoring frequency introduced to detect potential adverse effects earlier." // "Specifically implemented to decrease safety risks."
* extension[m11-protocol-amendment][=].extension[substantialImpactReliability].valueCodeableConcept = $NCIT#C49487 "No" //$NCIT#C49487  "No"


* extension[m11-protocol-amendment][=].extension[details][+].extension[detail].valueString = "Updated safety monitoring schedule — DSMB review frequency increased from 6-monthly to 3-monthly." //"Clarification"
* extension[m11-protocol-amendment][=].extension[details][=].extension[rationale].valueString = "Interim safety data indicated a need for more frequent safety oversight." //"Clarification of synopsis at request of regulator"
* extension[m11-protocol-amendment][=].extension[details][=].extension[section].valueCodeableConcept = $NCIT#C217349 "Section 8" //$NCIT#C218515  "ICH M11 Protocol Section 1.1 Protocol Synopsis"


* extension[m11-protocol-amendment][=].extension[details][+].extension[detail].valueString = "Rescue medication protocol clarified to include oral corticosteroids as second-line option." //"Clarification"
* extension[m11-protocol-amendment][=].extension[details][=].extension[rationale].valueString = "Feedback from investigator sites indicated ambiguity in the original wording." //"Clarification of synopsis at request of regulator"
* extension[m11-protocol-amendment][=].extension[details][=].extension[section].valueCodeableConcept = $NCIT#C217347 "Section 6" //$NCIT#C218515  "ICH M11 Protocol Section 1.1 Protocol Synopsis"




// * extension[m11-protocol-amendment][=].extension[rationale].valueString = "Updates to address safety concern & align with product guidelines."
// * extension[m11-protocol-amendment][=].extension[description].valueMarkdown = """#Protocol ABC-Exemplar A Phase 3 Study of Inhaled Exoticillin compared to Intramuscular Exoticillin for Treatment of Bronchtis Japanese Patients with Iatrogenic Diabetes Mellitus
//     has been amended.
//     The new protocol is indicated by *Amendment (a)* and will be used to conduct the study in place of any preceding version of the protocol. 
//     The overall changes and rationale for the changes made to this protocol are as follows: 
//     - An exclusion criterion for patients with retinopathy or maculopathy was added due to the potential risk of 
//     fundal hemorrhage induced by hypoglycemia. 
//     - The Intravenous Exoticillin GlucaGen reconstitution volume was changed from 1.0 mL to 1.1 mL in accordance with instructions in the Summary of Product Characteristics (2015).
//     """


Instance: InvestigationalMedicinalProduct
InstanceOf: MedicinalProductDefinition
Title: "Exemplar Medicinal Product"
Usage: #example
Description: """Illustration of a MedicinalProductDefinition used by the protocol
"""

* identifier[+].type.coding[+] = $NCIT#C218675 "Sponsor's Investigational Product Code"
* identifier[=].system = $SpID
* identifier[=].value = "EX2010/12-IH; EX2010/12-IM" //"EX2015/03"

* name[+].productName = "Exoticillin dry powder for inhalation; Exoticillin solution for injection" //"exoticillin 100micrograms/dose dry powder inhaler"
* name[=].type = $NCIT#C97054 "Nonproprietary Name(s)"
* name[+].productName = "Exoticillin Inhaler; Exoticillin Injector" //"Exotex 100 Inhaler"
* name[=].type = $NCIT#C71898 "Proprietary Name(s)"

//---------------------------------------------------------------
// Organisations

Instance: SponsorOrganization
InstanceOf: Organization
Title: "Exemplar Sponsor Organization"
Usage: #example
Description: "A minimal Organization definition"

* name = "Exemplar Pharmaceuticals Ltd" //"Exemplar Pharmaceuticals Co."
* contact[+].purpose.coding[+] = http://terminology.hl7.org/CodeSystem/contactentity-type#ADMIN "Administrative" //"Contact details for administrative enquiries."
* contact[=].address.line[+] = "1 Science Park, Cambridge, CB4 0WA, United Kingdom" //"Exemplar Road"

//=======================================
//

Instance: LocalSponsorOrganization
InstanceOf: Organization
Title: "Exemplar Local Sponsor Organization"
Usage: #example
Description: "A minimal Organization definition"

* name = "Exemplar Pharmaceuticals Japan K.K." //"Fictive Pharmaceuticals (Australia) Pty"
* contact[+].purpose.coding[+] = http://terminology.hl7.org/CodeSystem/contactentity-type#ADMIN "Administrative" //"Contact details for administrative enquiries."
* contact[=].address.line[+] = "2-1-1 Nihonbashi, Chuo-ku, Tokyo 103-0027, Japan"  //"Fictive High Road"


//=======================================
//

Instance: Exemplar-Regulator-Organization
InstanceOf: Organization
Title: "Exemplar Regulating Authority (ERA)"
Usage: #example
Description: "A minimal Regulator definition"

* name = "Exemplar Regulating Authority (ERA)"
* contact[+].purpose.coding[+] = http://terminology.hl7.org/CodeSystem/contactentity-type#ADMIN "Administrative" //"Contact details for administrative enquiries."
* contact[=].address.line[+] = "Exemplar Road"
* contact[=].address.city = "Exemplarton"
* contact[=].address.postalCode = "EX99 2AB"
* contact[=].address.country = "United Kingdom"

//---------------------------------------------------------------
// Practitioners

Instance: SponsorPractitioner
InstanceOf: Practitioner
Title: "Exemplar Sponsor Practitioner"
Usage: #example
Description: "A minimal Practitioner definition"

* name.text = "Exemplar Pharmaceuticals Ltd" //"Dr Exemplar Practitioner"
// assume Sponsor as an individual has the same legal address
* address[+].line[+] = "1 Science Park, Cambridge, CB4 0WA, United Kingdom" //"Exemplar Road"


//=======================================
//

Instance: LocalSponsorPractitioner
InstanceOf: Practitioner
Title: "Exemplar Local Sponsor Practitioner"
Usage: #example
Description: "A minimal Practitioner definition"

* name.text = "Exemplar Pharmaceuticals Japan K.K." //"Bruce Exemplar"
// assume Sponsor as an individual has the same legal address
* address[+].line[+] = "2-1-1 Nihonbashi, Chuo-ku, Tokyo 103-0027, Japan" //"Fictive High Road"

//=======================================
//

Instance: SponsorExpertPractitioner
InstanceOf: Practitioner
Title: "Exemplar Sponsor Expert Medical Practitioner"
Usage: #example
Description: "A minimal Practitioner definition"

* text.status = #generated
* text.div = "<div xmlns=\"http://www.w3.org/1999/xhtml\" lang = \"en\" xml:lang = \"en\">
  Medical Expert contact details held in the sponsor TMF
    </div>"
* name.text = "Dr James Brown, MD PhD, Exemplar Pharmaceuticals Ltd, james.brown@exemplarpharma.com" //"Dr Exemplar Consultant"
