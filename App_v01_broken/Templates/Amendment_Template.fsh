Instance: {{Protocol-Amendment}}
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
* identifier[=].value = "{{C132351}}" // "ABC-Exemplar"

* identifier[+].type.text = "Amendment Identifier"
* identifier[=].type.coding[+].system = $NCIT
* identifier[=].type.coding[=].code = #C218477
* identifier[=].system = $AmdID
* identifier[=].value = "{{C218477}}" //"ABC-Exemplar(a)"

* version = "{{C181232}}" //"(a)"
* extension[m11-research-study].extension[versionDate].valueDate = {{C93813}} //2017-10-01

//----------------------------------

//---------------------------------------------------------------
// Title Page Amemndment Summary
//

* extension[m11-protocol-amendment][+].extension[previous].valueCodeableConcept = $NCIT#C218488	"Protocol Previously Amended See Summary of Changes Before the Table of Contents"

* extension[m11-protocol-amendment][=].extension[scope].valueCodeableConcept = {{C218673}} //$NCIT#C217026	"Not Global"
* extension[m11-protocol-amendment][=].extension[region].valueCodeableConcept = {{C218674}} //$iso3166-2#AU-NSW "New South Wales"
//* extension[m11-protocol-amendment][=].extension[site][+].valueIdentifier.system = $AmdSite
* extension[m11-protocol-amendment][=].extension[site][=].valueIdentifier.value = {{C83081}} //"exemplarSite-14"



* extension[m11-protocol-amendment][=].extension[scopeImpact][+].extension[scope].valueCodeableConcept = {{C218673}} //$NCIT#C41065  "Locally"
// TODO These scopes are different
* extension[m11-protocol-amendment][=].extension[scopeImpact][+].extension[scope].valueCodeableConcept = {{C218695}} //$NCIT#C41065  "Locally"
* extension[m11-protocol-amendment][=].extension[scopeImpact][=].extension[number].valuePositiveInt = {{C218874}} //234
//* extension[m11-protocol-amendment][=].extension[scopeImpact][+].extension[scope].valueCodeableConcept = $NCIT#C68846  "Global"
//* extension[m11-protocol-amendment][=].extension[scopeImpact][=].extension[number].valuePositiveInt = 983

* extension[m11-protocol-amendment][=].extension[primaryReason].valueCodeableConcept = {{C218696}} //$NCIT#C218490  "Regulatory Agency Request to Amend Amendment Reason"
* extension[m11-protocol-amendment][=].extension[primaryReason][=].valueCodeableConcept.text = "{{C17649}}" //"Packaging revision"
* extension[m11-protocol-amendment][=].extension[secondaryReason][+].valueCodeableConcept = {{C218697}} //$NCIT#C218494  "Manufacturing Change Amendment Reason"
* extension[m11-protocol-amendment][=].extension[secondaryReason][=].valueCodeableConcept.text = "{{C17649}}" //"Packaging revision"
* extension[m11-protocol-amendment][=].extension[summary].valueString = "{{C42581}}" //"Manufacturing chanage to enable packaging change to recyclable materials."

* extension[m11-protocol-amendment][=].extension[substantialImpactSafety].valueCodeableConcept = {{C218698}} // $NCIT#C49488  "Yes"
* extension[m11-protocol-amendment][=].extension[substantialImpactSafetyComment].valueString = "{{C218699}}" // "Specifically implemented to decrease safety risks."
* extension[m11-protocol-amendment][=].extension[substantialImpactReliability].valueCodeableConcept = {{C218700}} //$NCIT#C49487  "No"
* extension[m11-protocol-amendment][=].extension[substantialImpactReliabilityComment].valueString = "{{C218701}}" // "Specifically implemented to decrease compliance risks."

{{REPEAT
* extension[m11-protocol-amendment][=].extension[details][+].extension[detail].valueString = "{{C218483}}" //"Clarification"
* extension[m11-protocol-amendment][=].extension[details][=].extension[rationale].valueString = "{{C181233}}" //"Clarification of synopsis at request of regulator"
* extension[m11-protocol-amendment][=].extension[details][=].extension[section].valueCodeableConcept = {{C218479}} //$NCIT#C218515  "ICH M11 Protocol Section 1.1 Protocol Synopsis"
}}




// * extension[m11-protocol-amendment][=].extension[rationale].valueString = "Updates to address safety concern & align with product guidelines."
// * extension[m11-protocol-amendment][=].extension[description].valueMarkdown = """#Protocol ABC-Exemplar A Phase 3 Study of Inhaled Exoticillin compared to Intramuscular Exoticillin for Treatment of Bronchtis Japanese Patients with Iatrogenic Diabetes Mellitus
//     has been amended.
//     The new protocol is indicated by *Amendment (a)* and will be used to conduct the study in place of any preceding version of the protocol. 
//     The overall changes and rationale for the changes made to this protocol are as follows: 
//     - An exclusion criterion for patients with retinopathy or maculopathy was added due to the potential risk of 
//     fundal hemorrhage induced by hypoglycemia. 
//     - The Intravenous Exoticillin GlucaGen reconstitution volume was changed from 1.0 mL to 1.1 mL in accordance with instructions in the Summary of Product Characteristics (2015).
//     """

