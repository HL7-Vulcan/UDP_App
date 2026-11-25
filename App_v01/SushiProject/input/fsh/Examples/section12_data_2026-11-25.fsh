Instance: Narrative-Composition-M11Section12
InstanceOf: m11-research-study-narratives
Title: "Example Narrative Single Composition with a Section for Each M11 Section"
Usage: #example
Description: """Example Narrative Single Composition with a contained section for e part of M11 Section 12.
"""

* status = #final
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"

/* 
* $NCIT#C218666  "ICH M11 Protocol Section 12 APPENDIX: SUPPORTING DETAILS"
* $NCIT#C218667  "ICH M11 Protocol Section 12.1 Clinical Laboratory Tests"
* $NCIT#C218668  "ICH M11 Protocol Section 12.2 Country/Region-Specific Differences"
* $NCIT#C218669  "ICH M11 Protocol Section 12.3 Prior Protocol Amendment(s)"
*/

      
* section[+]
  * title = "Section 12.1"
  * code = $NCIT#C218667  "ICH M11 Protocol Section 12.1 Clinical Laboratory Tests"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Standard haematology (full blood count with differential, platelets), biochemistry (sodium, potassium, urea, creatinine, eGFR, ALT, AST, alkaline phosphatase, total bilirubin, albumin, HbA1c, fasting glucose), and urinalysis (dipstick; microscopy if indicated) will be performed at a central laboratory at screening, Day 1 (pre-dose), Day 5, Day 10, and Day 24. All laboratory assays will be conducted in accordance with the central laboratory manual. Normal reference ranges will be provided by the central laboratory and will be used for clinical significance assessments.
        </div>
      """

* section[+]
  * title = "Section 12.2"
  * code = $NCIT#C218668  "ICH M11 Protocol Section 12.2 Country/Region-Specific Differences"
  * entry[+] = Reference(Section12-2)


* section[+]
  * title = "Section 12.3"
  * code = $NCIT#C218669  "ICH M11 Protocol Section 12.3 Prior Protocol Amendment(s)"
 // * extension[m11-research-study].extension[originalProtocol].valueCodeableConcept = $NCIT#C49487 "No"  //$NCIT#C21848 "Protocol Previously Amended See Summary of Changes Before the Table of Contents"
  * entry[+] = Reference(Section12-3a)

  * entry[+] = Reference(Section12-3b) 

//-------------------------------------------------------------------------------------
//
Instance: Section12-2
InstanceOf: m11-research-study-profile
Title: "Current amendments details"
Usage: #example
Description: """
xxx
"""

* status = #active
* id = "Section12-2" // This is specified ONLY to allow documentatation of examples - it is not normally specified

* identifier[+].type.coding[+] = $NCIT#C132351 "Sponsor Protocol Identifier"
* identifier[=].system = $SpID

* identifier[=].type.coding[+] = $NCIT#C218477 "Amendment Identifier"
* identifier[=].system = $AmdID

* extension[m11-research-study].extension[originalProtocol].valueCodeableConcept = $NCIT#C49487 "No" 
* extension[m11-research-study].extension[versionDate].valueDate = 2024-11-06 //2017-10-01



* extension[m11-research-study].extension[differences][+].extension[country].valueCodeableConcept = $iso3166#JP "Japan" // $iso3166-2#DE-Germany "Germany" 
* extension[m11-research-study].extension[differences][=].extension[requirements].valueString = "Additional pharmacokinetic (PK) blood sampling required to fulfil Japanese regulatory guidelines (MHLW) for anti-infective bridging studies."
* extension[m11-research-study].extension[differences][=].extension[clarification].valueString = "PK samples to be collected at 0.5, 1, 2, 4, 6, and 8 hours post-dose on Day 5 at Japanese sites, in addition to the standard schedule."




* extension[m11-research-study].extension[differences][+].extension[country].valueCodeableConcept = $iso3166#DE "Germany" // $iso3166-2#DE-Germany "Germany" 
* extension[m11-research-study].extension[differences][=].extension[requirements].valueString = "Additional liver-function monitoring required at German sites following BfArM guidance on hepatotoxicity surveillance for the Exoticillin class."
* extension[m11-research-study].extension[differences][=].extension[clarification].valueString = "ALT, AST, and total bilirubin to be measured at Weeks 2, 4, and 8 at German sites (DE-001 through DE-007), in addition to the standard schedule."


//-------------------------------------------------------------------------------------
//

//-------------------------------------------------------------------------------------
//
Instance: Section12-3a
InstanceOf: m11-research-study-profile
Title: "Current amendments details"
Usage: #example
Description: """
xxx
"""
* status = #active
* id = "Section12-3a" // This is specified ONLY to allow documentatation of examples - it is not normally specified

* identifier[+].type.coding[+] = $NCIT#C132351 "Sponsor Protocol Identifier"
* identifier[=].system = $SpID

* identifier[=].type.coding[+] = $NCIT#C218477 "Amendment Identifier"
* identifier[=].system = $AmdID



* extension[m11-research-study].extension[m11-protocol-amendment][+].extension[identifier][+].valueIdentifier.type.coding[+] = $NCIT#C218477 "Amendment Identifier"
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[identifier][=].valueIdentifier.system = $AmdID
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[identifier][=].valueIdentifier.value = "AMD-001" //"ABC-Exemplar(a)"
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[scope].valueCodeableConcept = $NCIT#C68846 "Global" //$NCIT#C217026	"Not Global"

* extension[m11-research-study].extension[originalProtocol].valueCodeableConcept = $NCIT#C49487 "No" 
* extension[m11-research-study].extension[versionDate].valueDate = 2025-03-14 //2017-10-01

// Approx. Enrolled at Time of Sponsor Approval:
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[scopeImpact][+].extension[number].valuePositiveInt = 120 //234
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[scopeImpact][=].extension[scope].valueCodeableConcept = $NCIT#C68846 "Globally" //$NCIT#C41065  "Locally"




* extension[m11-research-study].extension[m11-protocol-amendment][+].extension[identifier][+].valueIdentifier.type.coding[+] = $NCIT#C218477 "Amendment Identifier"
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[identifier][=].valueIdentifier.system = $AmdID
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[identifier][=].valueIdentifier.value = "AMD-002" //"ABC-Exemplar(a)"
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[scope].valueCodeableConcept = $NCIT#C217026 "Not Global" //$NCIT#C217026	"Not Global"

* extension[m11-research-study].extension[originalProtocol].valueCodeableConcept = $NCIT#C49487 "No" 
* extension[m11-research-study].extension[versionDate].valueDate = 2025-09-01 //2017-10-01

// Approx. Enrolled at Time of Sponsor Approval:
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[scopeImpact][+].extension[number].valuePositiveInt = 187 //234
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[scopeImpact][=].extension[scope].valueCodeableConcept = $NCIT#C41065 "Locally" //$NCIT#C41065  "Locally"




* extension[m11-research-study].extension[m11-protocol-amendment][+].extension[identifier][+].valueIdentifier.type.coding[+] = $NCIT#C218477 "Amendment Identifier"
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[identifier][=].valueIdentifier.system = $AmdID
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[identifier][=].valueIdentifier.value = "AMD-003" //"ABC-Exemplar(a)"
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[scope].valueCodeableConcept = $NCIT#C217026 "Not Global" //$NCIT#C217026	"Not Global"

* extension[m11-research-study].extension[originalProtocol].valueCodeableConcept = $NCIT#C49487 "No" 
* extension[m11-research-study].extension[versionDate].valueDate = 2026-01-20 //2017-10-01

// Approx. Enrolled at Time of Sponsor Approval:
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[scopeImpact][+].extension[number].valuePositiveInt = 14 //234
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[scopeImpact][=].extension[scope].valueCodeableConcept = $NCIT#C41065 "Locally" //$NCIT#C41065  "Locally"


//-------------------------------------------------------------------------------------
//

//-------------------------------------------------------------------------------------
//
Instance: Section12-3b
InstanceOf: m11-research-study-profile
Title: "Current amendments details"
Usage: #example
Description: """
xxx
"""
* status = #active
* id = "Section12-3b" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* identifier[+].type.coding[+] = $NCIT#C132351 "Sponsor Protocol Identifier"
* identifier[=].system = $SpID

* identifier[=].type.coding[+] = $NCIT#C218477 "Amendment Identifier"
* identifier[=].system = $AmdID

* extension[m11-research-study].extension[m11-protocol-amendment][+].extension[identifier][+].valueIdentifier.type.coding[+] = $NCIT#C218477 "Amendment Identifier"
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[identifier][=].valueIdentifier.system = $AmdID
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[scope].valueCodeableConcept = $NCIT#C217026 "Not Global" //$NCIT#C217026	"Not Global"

* extension[m11-research-study].extension[originalProtocol].valueCodeableConcept = $NCIT#C49487 "No" 
* extension[m11-research-study].extension[versionDate].valueDate = 2026-01-20 //2017-10-01

* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[detailsLocation].valueString = "under the bed"



