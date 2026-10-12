Instance: Narrative-Composition-M11Section03
InstanceOf: m11-research-study-narratives
Title: "Example Narrative Single Composition with a Section for Each M11 Section"
Usage: #example
Description: """Example Narrative Single Composition with a contained section for e part of M11 Section 03.
"""

* status = #final
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"

/* 
* $NCIT#C218526  "ICH M11 Protocol Section 3 TRIAL OBJECTIVES AND ASSOCIATED ESTIMANDS"
* $NCIT#C218527  "ICH M11 Protocol Section 3.1 Primary Objective(s) and Associated Estimand(s)"
* $NCIT#C218528  "ICH M11 Protocol Section 3.1.1 Primary Objective"
* $NCIT#C218529  "ICH M11 Protocol Section 3.2 Secondary Objective(s) and Associated Estimand(s)"
* $NCIT#C218530  "ICH M11 Protocol Section 3.2.1 Secondary Objective"
* $NCIT#C218531  "ICH M11 Protocol Section 3.3 Exploratory Objective(s)"
* $NCIT#C218532  "ICH M11 Protocol Section 3.3.1 Exploratory Objective"
*/

/*
Section_03_Template.fsh (Narrative-Composition-M11Section03)
    REPEAT: Section_03_Primary_Template.fsh (OBJECTIVE_TABLE_X)
        3.1.1 Primary Objective
        Section_03_Primary_Estimands_Template.fsh (ESTIMANDS_TABLE_X)
        REPEAT: Section_03_Primary_Events_Template.fsh (INTERCURRENTEVENTS_ROW_X)
*/




* section[+]
  * title = "Section 3.1"
  * code = $NCIT#C218527  "ICH M11 Protocol Section 3.1 Primary Objective(s) and Associated Estimand(s)"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  Sample objective
        </div>
     """

  * section[+]
    * title = "Section 3.1.1"
    * code = $NCIT#C218528  "ICH M11 Protocol Section 3.1.1 Primary Objective"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
primary
        </div>
     """
    * entry[+] = Reference(Estimands-primary-01)
    * entry[+] = Reference(ICE-primary-01-01) 
 


// ── Primary Objective — Estimands & ICE Instances ──────────────────────
//--------------------------------------------------------------------------------------
//
Instance: Estimands-primary-01
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "Estimands-primary-01" // This is specified ONLY to allow documentatation of examples - it is not normally specified
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

* extension[objective][+].extension[estimands][+].extension[population].valueString = "diabetes"
 
* extension[objective][=].extension[estimands][+].extension[treatment].valueString[+] = "exoticillin"
* extension[objective][=].extension[estimands][+].extension[endpoint].valueString = "improvement"
* extension[objective][=].extension[estimands][+].extension[summary].valueString = "large"


//--------------------------------------------------------------------------------------
//
Instance: ICE-primary-01-01
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "ICE-primary-01-01" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
  event 1..1 MS
  strategy 1..1 MS
*/

* extension[objective][+].extension[intercurrentEvents][+].extension[event].valueString = "ice_e1"
* extension[objective][=].extension[intercurrentEvents][=].extension[strategy].valueString = "ice_s1"








