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
        </div>
     """

  * section[+]
    * title = "Section 3.1.1"
    * code = $NCIT#C218528  "ICH M11 Protocol Section 3.1.1 Primary Objective"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
~~~pobj1
        </div>
     """
    * entry[+] = Reference(Estimands-primary-01)
    * entry[+] = Reference(ICE-primary-01-01)
    * entry[+] = Reference(ICE-primary-01-02)

  * section[+]
    * title = "Section 3.1.1"
    * code = $NCIT#C218528  "ICH M11 Protocol Section 3.1.1 Primary Objective"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
~~~pobj2
        </div>
     """
    * entry[+] = Reference(Estimands-primary-02)
    * entry[+] = Reference(ICE-primary-02-01)
    * entry[+] = Reference(ICE-primary-02-02) 
 
* section[+]
  * title = "Section 3.2"
  * code = $NCIT#C218529  "ICH M11 Protocol Section 3.2 Secondary Objective(s) and Associated Estimand(s)"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
        </div>
     """

  * section[+]
    * title = "Section 3.2.1"
    * code = $NCIT#C218530  "ICH M11 Protocol Section 3.2.1 Secondary Objective"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
~~~sobj1
        </div>
     """
    * entry[+] = Reference(Estimands-secondary-01)
    * entry[+] = Reference(ICE-secondary-01-01)
    * entry[+] = Reference(ICE-secondary-01-02)

  * section[+]
    * title = "Section 3.2.1"
    * code = $NCIT#C218530  "ICH M11 Protocol Section 3.2.1 Secondary Objective"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
~~~sobj2
        </div>
     """
    * entry[+] = Reference(Estimands-secondary-02)
    * entry[+] = Reference(ICE-secondary-02-01)
    * entry[+] = Reference(ICE-secondary-02-02)
* section[+]
  * title = "Section 3.3"
  * code = $NCIT#C218531  "ICH M11 Protocol Section 3.3 Exploratory Objective(s)"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
        </div>
     """

  * section[+]
    * title = "Section 3.3.1"
    * code = $NCIT#C218532  "ICH M11 Protocol Section 3.3.1 Exploratory Objective"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
~~~xobj1
        </div>
     """
    * entry[+] = Reference(Estimands-exploratory-01)
    * entry[+] = Reference(ICE-exploratory-01-01)
    * entry[+] = Reference(ICE-exploratory-01-02)

  * section[+]
    * title = "Section 3.3.1"
    * code = $NCIT#C218532  "ICH M11 Protocol Section 3.3.1 Exploratory Objective"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
~~~xobj2
        </div>
     """
    * entry[+] = Reference(Estimands-exploratory-02)
    * entry[+] = Reference(ICE-exploratory-02-01)
    * entry[+] = Reference(ICE-exploratory-02-02)

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

* extension[objective][+].extension[estimands][+].extension[population].valueString = "~~~pop_est1_pobj1"
 
* extension[objective][=].extension[estimands][+].extension[treatment].valueString[+] = "~~~treat1_est1_pobj1
~~~treat2_est1_pobj1"
* extension[objective][=].extension[estimands][+].extension[endpoint].valueString = "~~~end_est1_pobj1"
* extension[objective][=].extension[estimands][+].extension[summary].valueString = "~~~sum_est1_pobj1"


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

* extension[objective][+].extension[intercurrentEvents][+].extension[event].valueString = "~~~event_ice1_pobj1"
* extension[objective][=].extension[intercurrentEvents][=].extension[strategy].valueString = "~~~strat_ice1_pobj1"

//--------------------------------------------------------------------------------------
//
Instance: ICE-primary-01-02
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "ICE-primary-01-02" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
  event 1..1 MS
  strategy 1..1 MS
*/

* extension[objective][+].extension[intercurrentEvents][+].extension[event].valueString = "~~~event_ice2_pobj1"
* extension[objective][=].extension[intercurrentEvents][=].extension[strategy].valueString = "~~~strat_ice2_pobj1"

//--------------------------------------------------------------------------------------
//
Instance: Estimands-primary-02
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "Estimands-primary-02" // This is specified ONLY to allow documentatation of examples - it is not normally specified
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

* extension[objective][+].extension[estimands][+].extension[population].valueString = "~~~pop_est1_pobj2"
 
* extension[objective][=].extension[estimands][+].extension[treatment].valueString[+] = "~~~treat1_est1_pobj2
~~~treat2_est1_pobj2"
* extension[objective][=].extension[estimands][+].extension[endpoint].valueString = "~~~end_est1_pobj2"
* extension[objective][=].extension[estimands][+].extension[summary].valueString = "~~~sum_est1_pobj2"


//--------------------------------------------------------------------------------------
//
Instance: ICE-primary-02-01
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "ICE-primary-02-01" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
  event 1..1 MS
  strategy 1..1 MS
*/

* extension[objective][+].extension[intercurrentEvents][+].extension[event].valueString = "~~~event_ice1_pobj2"
* extension[objective][=].extension[intercurrentEvents][=].extension[strategy].valueString = "~~~strat_ice1_pobj2"

//--------------------------------------------------------------------------------------
//
Instance: ICE-primary-02-02
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "ICE-primary-02-02" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
  event 1..1 MS
  strategy 1..1 MS
*/

* extension[objective][+].extension[intercurrentEvents][+].extension[event].valueString = "~~~event_ice2_pobj2"
* extension[objective][=].extension[intercurrentEvents][=].extension[strategy].valueString = "~~~strat_ice2_pobj2"


// ── Secondary Objective — Estimands & ICE Instances ──────────────────────
//--------------------------------------------------------------------------------------
//
Instance: Estimands-secondary-01
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "Estimands-secondary-01" // This is specified ONLY to allow documentatation of examples - it is not normally specified
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

* extension[objective][+].extension[estimands][+].extension[population].valueString = "~~~pop_est1_sobj1"
 
* extension[objective][=].extension[estimands][+].extension[treatment].valueString[+] = "~~~treat1_est1_sobj1
~~~treat2_est1_sobj1"
* extension[objective][=].extension[estimands][+].extension[endpoint].valueString = "~~~end_est1_sobj1"
* extension[objective][=].extension[estimands][+].extension[summary].valueString = "~~~sum_est1_sobj1"


//--------------------------------------------------------------------------------------
//
Instance: ICE-secondary-01-01
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "ICE-secondary-01-01" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
  event 1..1 MS
  strategy 1..1 MS
*/

* extension[objective][+].extension[intercurrentEvents][+].extension[event].valueString = "~~~event_ice1_sobj1"
* extension[objective][=].extension[intercurrentEvents][=].extension[strategy].valueString = "~~~strat_ice1_sobj1"

//--------------------------------------------------------------------------------------
//
Instance: ICE-secondary-01-02
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "ICE-secondary-01-02" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
  event 1..1 MS
  strategy 1..1 MS
*/

* extension[objective][+].extension[intercurrentEvents][+].extension[event].valueString = "~~~event_ice2_sobj1"
* extension[objective][=].extension[intercurrentEvents][=].extension[strategy].valueString = "~~~strat_ice2_sobj1"

//--------------------------------------------------------------------------------------
//
Instance: Estimands-secondary-02
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "Estimands-secondary-02" // This is specified ONLY to allow documentatation of examples - it is not normally specified
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

* extension[objective][+].extension[estimands][+].extension[population].valueString = "~~~pop_est1_sobj2"
 
* extension[objective][=].extension[estimands][+].extension[treatment].valueString[+] = "~~~treat1_est1_sobj2
~~~treat2_est1_sobj2"
* extension[objective][=].extension[estimands][+].extension[endpoint].valueString = "~~~end_est1_sobj2"
* extension[objective][=].extension[estimands][+].extension[summary].valueString = "~~~sum_est1_sobj2"


//--------------------------------------------------------------------------------------
//
Instance: ICE-secondary-02-01
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "ICE-secondary-02-01" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
  event 1..1 MS
  strategy 1..1 MS
*/

* extension[objective][+].extension[intercurrentEvents][+].extension[event].valueString = "~~~event_ice1_sobj2"
* extension[objective][=].extension[intercurrentEvents][=].extension[strategy].valueString = "~~~strat_ice1_sobj2"

//--------------------------------------------------------------------------------------
//
Instance: ICE-secondary-02-02
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "ICE-secondary-02-02" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
  event 1..1 MS
  strategy 1..1 MS
*/

* extension[objective][+].extension[intercurrentEvents][+].extension[event].valueString = "~~~event_ice2_sobj2"
* extension[objective][=].extension[intercurrentEvents][=].extension[strategy].valueString = "~~~strat_ice2_sobj2"


// ── Exploratory Objective — Estimands & ICE Instances ──────────────────────
//--------------------------------------------------------------------------------------
//
Instance: Estimands-exploratory-01
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "Estimands-exploratory-01" // This is specified ONLY to allow documentatation of examples - it is not normally specified
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

* extension[objective][+].extension[estimands][+].extension[population].valueString = "~~~pop_est1_xobj1"
 
* extension[objective][=].extension[estimands][+].extension[treatment].valueString[+] = "~~~treat1_est1_xobj1
~~~treat2_est1_xobj1"
* extension[objective][=].extension[estimands][+].extension[endpoint].valueString = "~~~end_est1_xobj1"
* extension[objective][=].extension[estimands][+].extension[summary].valueString = "~~~sum_est1_xobj1"


//--------------------------------------------------------------------------------------
//
Instance: ICE-exploratory-01-01
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "ICE-exploratory-01-01" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
  event 1..1 MS
  strategy 1..1 MS
*/

* extension[objective][+].extension[intercurrentEvents][+].extension[event].valueString = "~~~event_ice1_xobj1"
* extension[objective][=].extension[intercurrentEvents][=].extension[strategy].valueString = "~~~strat_ice1_xobj1"

//--------------------------------------------------------------------------------------
//
Instance: ICE-exploratory-01-02
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "ICE-exploratory-01-02" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
  event 1..1 MS
  strategy 1..1 MS
*/

* extension[objective][+].extension[intercurrentEvents][+].extension[event].valueString = "~~~event_ice2_xobj1"
* extension[objective][=].extension[intercurrentEvents][=].extension[strategy].valueString = "~~~strat_ice2_xobj1"

//--------------------------------------------------------------------------------------
//
Instance: Estimands-exploratory-02
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "Estimands-exploratory-02" // This is specified ONLY to allow documentatation of examples - it is not normally specified
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

* extension[objective][+].extension[estimands][+].extension[population].valueString = "~~~pop_est1_xobj2"
 
* extension[objective][=].extension[estimands][+].extension[treatment].valueString[+] = "~~~treat1_est1_xobj2
~~~treat2_est1_xobj2"
* extension[objective][=].extension[estimands][+].extension[endpoint].valueString = "~~~end_est1_xobj2"
* extension[objective][=].extension[estimands][+].extension[summary].valueString = "~~~sum_est1_xobj2"


//--------------------------------------------------------------------------------------
//
Instance: ICE-exploratory-02-01
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "ICE-exploratory-02-01" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
  event 1..1 MS
  strategy 1..1 MS
*/

* extension[objective][+].extension[intercurrentEvents][+].extension[event].valueString = "~~~event_ice1_xobj2"
* extension[objective][=].extension[intercurrentEvents][=].extension[strategy].valueString = "~~~strat_ice1_xobj2"

//--------------------------------------------------------------------------------------
//
Instance: ICE-exploratory-02-02
InstanceOf:  m11-objective-table
Usage: #example

Description: "Table defining the primary objectives"
* id = "ICE-exploratory-02-02" // This is specified ONLY to allow documentatation of examples - it is not normally specified
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* status = #draft


/*
  event 1..1 MS
  strategy 1..1 MS
*/

* extension[objective][+].extension[intercurrentEvents][+].extension[event].valueString = "~~~event_ice2_xobj2"
* extension[objective][=].extension[intercurrentEvents][=].extension[strategy].valueString = "~~~strat_ice2_xobj2"

