//--------------------------------------------------------------------------------------
//
Instance: {{TABLE_ROW_X}}
InstanceOf: Group
Usage: #example
Description: "Simple Eligibility criteria - Age 30-60 years + BMI>=35"
* id = "{{TABLE_ROW_X}}" // This is specified ONLY to allow documentatation of examples - it is not normally specified

// * identifier.type = $v2-0203#FILL "Filler Identifier"
// * identifier.system = $ID-EligibilityCriteria
// * identifier.value = "172461"

* name = "{{name}}"
* title = "{{title}}"
* status = #active

* description = "{{description}}"
* type = {{type}} //#person
* membership = {{membership}} //#definitional
* combinationMethod = {{combinationMethod}} //#all-of

REPEAT{{
* characteristic[+].code = {{"code"}} //$SCT#397669002 "Age"
* characteristic[=].valueRange.low = {{"low"}} //30 'a' "years"
* characteristic[=].valueRange.high = {{"high"}} //60 'a' "years"
* characteristic[=].exclude = {{exclude}} false
* characteristic[=].description = {{"description"}} //"aged 30-60 years"
}}
