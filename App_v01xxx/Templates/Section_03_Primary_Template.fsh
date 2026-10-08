//--------------------------------------------------------------------------------------
//
Instance: {{PRIMARY_OBJECTIVE_TABLE_X}}
InstanceOf: m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "{{PRIMARY_OBJECTIVE_TABLE_X}}" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
* $NCIT#C218528  "ICH M11 Protocol Section 3.1.1 Primary Objective"

        3.1.1 Primary Objective
        Section_03_Primary_Estimands_Template.fsh (ESTIMANDS_TABLE_X)
        REPEAT: Section_03_Primary_Events_Template.fsh (INTERCURRENTEVENTS_ROW_X)
*/

* section[+]
  * title = "Section 3.1.1"
  * code = $NCIT#C218528  "ICH M11 Protocol Section 3.1.1 Primary Objective"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C85826}}
        </div>
     """
  * entry[+] = Reference({{ESTIMANDS_TABLE_X}})
{{REPEAT  * entry[+] = Reference({{INTERCURRENTEVENTS_ROW_X}})}}

//--------------------------------------------------------------------------------------
//
Instance: {{SECONDARY_OBJECTIVE_TABLE_X}}
InstanceOf: m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "{{SECONDARY_OBJECTIVE_TABLE_X}}" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
* $NCIT#C218530  "ICH M11 Protocol Section 3.2.1 Secondary Objective"

        2.1 Secondary Objective
        Section_03_Primary_Estimands_Template.fsh (ESTIMANDS_TABLE_X)
        REPEAT: Section_03_Primary_Events_Template.fsh (INTERCURRENTEVENTS_ROW_X)
*/

* section[+]
  * title = "Section 3.2.1"
  * code = $NCIT#C218530  "ICH M11 Protocol Section 3.2.1 Secondary Objective"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C85827}}
        </div>
     """
  * entry[+] = Reference({{ESTIMANDS_TABLE_X}})
{{REPEAT  * entry[+] = Reference({{INTERCURRENTEVENTS_ROW_X}})}}

//--------------------------------------------------------------------------------------
//
Instance: {{EXPLORATORY_OBJECTIVE_TABLE_X}}
InstanceOf: m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "{{EXPLORATORY_OBJECTIVE_TABLE_X}}" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*

* $NCIT#C218532  "ICH M11 Protocol Section 3.3.1 Exploratory Objective"

        3.3.1 Exploratory Objective
        Section_03_Primary_Estimands_Template.fsh (ESTIMANDS_TABLE_X)
        REPEAT: Section_03_Primary_Events_Template.fsh (INTERCURRENTEVENTS_ROW_X)
*/

* section[+]
  * title = "Section 3.3.1"
  * code = $NCIT#C218532  "ICH M11 Protocol Section 3.3.1 Exploratory Objective"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C163559}}
        </div>
     """
  * entry[+] = Reference({{ESTIMANDS_TABLE_X}})
{{REPEAT  * entry[+] = Reference({{INTERCURRENTEVENTS_ROW_X}})}}



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

* extension[objective].extension[estimands].extension[population].valueString = "{{C70833}}"
{{REPEAT* extension[estimands].extension[population].valueString = "{{C49236}}"}}
* extension[objective].extension[estimands].extension[population].valueString = "{{C25212}}"
* extension[objective].extension[estimands].extension[population].valueString = "{{C188853}}"

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

* extension[objective].extension[intercurrentEvents].extension[event].valueString = "{{C188856}}"
* extension[objective].extension[intercurrentEvents].extension[strategy].valueString = "{{C188857}}"