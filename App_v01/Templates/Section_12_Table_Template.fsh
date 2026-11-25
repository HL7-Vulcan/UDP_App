//-------------------------------------------------------------------------------------
//
Instance: {{TABLE12.2_ROW_X}}
InstanceOf: m11-research-study-profile
Title: "Current amendments details"
Usage: #example
Description: """
xxx
"""

* status = #active
* id = "{{TABLE12.2_ROW_X}}" // This is specified ONLY to allow documentatation of examples - it is not normally specified

* identifier[+].type.coding[+] = $NCIT#C132351 "Sponsor Protocol Identifier"
* identifier[=].system = $SpID
* identifier[=].value = "{{C132351}}" // "ABC-Exemplar"

* identifier[=].type.coding[+] = $NCIT#C218477 "Amendment Identifier"
* identifier[=].system = $AmdID
* identifier[=].value = "{{C218477}}" 

* extension[m11-research-study].extension[originalProtocol].valueCodeableConcept = {{C218672}} 
* extension[m11-research-study].extension[versionDate].valueDate = {{C93813}} //2017-10-01

{{REPEAT

* extension[m11-research-study].extension[differences][+].extension[country].valueCodeableConcept = {{C218832}} // $iso3166-2#DE-Germany "Germany" 
* extension[m11-research-study].extension[differences][=].extension[requirements].valueString = "{{C218833}}"
* extension[m11-research-study].extension[differences][=].extension[clarification].valueString = "{{C218834}}"

}}

//-------------------------------------------------------------------------------------
//
Instance: {{TABLE12.3a_ROW_X}}
InstanceOf: m11-research-study-profile
Title: "Current amendments details"
Usage: #example
Description: """
xxx
"""
* status = #active
* id = "{{TABLE12.3a_ROW_X}}" // This is specified ONLY to allow documentatation of examples - it is not normally specified

* identifier[+].type.coding[+] = $NCIT#C132351 "Sponsor Protocol Identifier"
* identifier[=].system = $SpID
* identifier[=].value = "{{C132351}}" // "ABC-Exemplar"

* identifier[=].type.coding[+] = $NCIT#C218477 "Amendment Identifier"
* identifier[=].system = $AmdID
* identifier[=].value = "{{C218477}}" 

{{REPEAT

* extension[m11-research-study].extension[m11-protocol-amendment][+].extension[identifier][+].valueIdentifier.type.coding[+] = $NCIT#C218477 "Amendment Identifier"
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[identifier][=].valueIdentifier.system = $AmdID
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[identifier][=].valueIdentifier.value = "{{C218477}}" //"ABC-Exemplar(a)"
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[scope].valueCodeableConcept = {{C218673}} //$NCIT#C217026	"Not Global"

* extension[m11-research-study].extension[originalProtocol].valueCodeableConcept = {{C218672}} 
* extension[m11-research-study].extension[versionDate].valueDate = {{C93813}} //2017-10-01

// Approx. Enrolled at Time of Sponsor Approval:
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[scopeImpact][+].extension[number].valuePositiveInt = {{C218478}} //234
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[scopeImpact][=].extension[scope].valueCodeableConcept = {{C218695}} //$NCIT#C41065  "Locally"

}}

//-------------------------------------------------------------------------------------
//
Instance: {{TABLE12.3b_ROW_X}}
InstanceOf: m11-research-study-profile
Title: "Current amendments details"
Usage: #example
Description: """
xxx
"""
* status = #active
* id = "{{TABLE12.3b_ROW_X}}" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* identifier[+].type.coding[+] = $NCIT#C132351 "Sponsor Protocol Identifier"
* identifier[=].system = $SpID
* identifier[=].value = "{{C132351}}" // "ABC-Exemplar"

* identifier[=].type.coding[+] = $NCIT#C218477 "Amendment Identifier"
* identifier[=].system = $AmdID
* identifier[=].value = "{{C218477}}" 

{{OPTION 1
* extension[m11-research-study].extension[m11-protocol-amendment][+].extension[identifier][+].valueIdentifier.type.coding[+] = $NCIT#C218477 "Amendment Identifier"
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[identifier][=].valueIdentifier.system = $AmdID
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[identifier][=].valueIdentifier.value = "{{C218477}}" //"ABC-Exemplar(a)"
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[scope].valueCodeableConcept = {{C218673}} //$NCIT#C217026	"Not Global"

* extension[m11-research-study].extension[originalProtocol].valueCodeableConcept = {{C218672}} 
* extension[m11-research-study].extension[versionDate].valueDate = {{C93813}} //2017-10-01

* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[detailsLocation].valueString = "{{C218836}}"

}}

{{OPTION 2

{{REPEAT

* extension[m11-research-study].extension[m11-protocol-amendment][+].extension[identifier][+].valueIdentifier.type.coding[+] = $NCIT#C218477 "Amendment Identifier"
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[identifier][=].valueIdentifier.system = $AmdID
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[identifier][=].valueIdentifier.value = "{{C218477}}" //"ABC-Exemplar(a)"
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[scope].valueCodeableConcept = {{C218673}} //$NCIT#C217026	"Not Global"
* extension[m11-research-study].extension[originalProtocol].valueCodeableConcept = {{C218672}} 
* extension[m11-research-study].extension[m11-approval].extension[approvalDate].valueDate = {{C132352}}
* extension[m11-research-study].extension[versionDate].valueDate = {{C93813}} //2017-10-01

// Table Overview of Changes in the Current Amendment
{{REPEAT

* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[details][+].extension[detail].valueString = "{{C218483}}" //"Clarification"
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[details][=].extension[rationale].valueString = "{{C181233}}" //"Clarification of synopsis at request of regulator"
* extension[m11-research-study].extension[m11-protocol-amendment][=].extension[details][=].extension[section].valueCodeableConcept = {{C218479}} //$NCIT#C218515  "ICH M11 Protocol Section 1.1 Protocol Synopsis"

}}

}}

}}