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
{{REPEAT
  * section[+]
    * title = "Section 3.1.1"
    * code = $NCIT#C218528  "ICH M11 Protocol Section 3.1.1 Primary Objective"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C85826}}
        </div>
     """
    * entry[+] = Reference({{ESTIMANDS_TABLE_X}})
  {{REPEAT    * entry[+] = Reference({{INTERCURRENTEVENTS_ROW_X}})}}
}} 
 
 
* section[+]
  * title = "Section 3.2"
  * code = $NCIT#C218529  "ICH M11 Protocol Section 3.2 Secondary Objective(s) and Associated Estimand(s)"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
        </div>
     """
{{REPEAT
  * section[+]
    * title = "Section 3.2.1"
    * code = $NCIT#C218530  "ICH M11 Protocol Section 3.2.1 Secondary Objective"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C85827}}
        </div>
     """
    * entry[+] = Reference({{ESTIMANDS_TABLE_X}})
  {{REPEAT    * entry[+] = Reference({{INTERCURRENTEVENTS_ROW_X}})}}
}} 

* section[+]
  * title = "Section 3.3"
  * code = $NCIT#C218531  "ICH M11 Protocol Section 3.3 Exploratory Objective(s)"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
        </div>
     """
{{REPEAT
  * section[+]
    * title = "Section 3.3.1"
    * code = $NCIT#C218532  "ICH M11 Protocol Section 3.3.1 Exploratory Objective"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C163559}}
        </div>
     """
    * entry[+] = Reference({{ESTIMANDS_TABLE_X}})
  {{REPEAT    * entry[+] = Reference({{INTERCURRENTEVENTS_ROW_X}})}}
}} 
