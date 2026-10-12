//--------------------------------------------------------------------------------------
//
Instance: {{ESTIMANDS_TABLE_X}}
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "{{PRIMARY_TABLE_X}}" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
  population 0..1 MS
  treatment 0..* MS
  endpoint 1..1 MS
  summary 0..1
*/

* extension[objective][+].extension[estimands][+].extension[population].valueString = "{{C70833}}"
{{REPEAT 
* extension[objective][=].extension[estimands][+].extension[treatment].valueString[+] = "{{C49236}}"
}}
* extension[objective][=].extension[estimands][+].extension[endpoint].valueString = "{{C25212}}"
* extension[objective][=].extension[estimands][+].extension[summary].valueString = "{{C188853}}"

//--------------------------------------------------------------------------------------
//
Instance: {{INTERCURRENTEVENTS_ROW_X}}
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "{{INTERCURRENTEVENTS_ROW_X}}" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
  event 1..1 MS
  strategy 1..1 MS
*/

* extension[objective][+].extension[intercurrentEvents][+].extension[event].valueString = "{{C188856}}"
* extension[objective][=].extension[intercurrentEvents][=].extension[strategy].valueString = "{{C188857}}"