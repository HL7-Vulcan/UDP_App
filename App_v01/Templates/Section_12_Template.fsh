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
{{C25294}}
        </div>
      """

* section[+]
  * title = "Section 12.2"
  * code = $NCIT#C218668  "ICH M11 Protocol Section 12.2 Country/Region-Specific Differences"
  * entry[+] = Reference({{TABLE12.2_ROW_X}})


* section[+]
  * title = "Section 12.3"
  * code = $NCIT#C218669  "ICH M11 Protocol Section 12.3 Prior Protocol Amendment(s)"
 // * extension[m11-research-study].extension[originalProtocol].valueCodeableConcept = {{C218672}}  //$NCIT#C21848 "Protocol Previously Amended See Summary of Changes Before the Table of Contents"
  * entry[+] = Reference({{TABLE12.3a_ROW_X}})

  * entry[+] = Reference({{TABLE12.3b_ROW_X}}) 
