Instance: Narrative-Composition-M11Section07
InstanceOf: m11-research-study-narratives
Title: "Example Narrative Single Composition with a Section for Each M11 Section"
Usage: #example
Description: """Example Narrative Single Composition with a contained section for e part of M11 Section 07.
"""

* status = #final
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"

/* 
* $NCIT#C218583  "ICH M11 Protocol Section 7 PARTICIPANT DISCONTINUATION OF TRIAL INTERVENTION AND DISCONTINUATION OR WITHDRAWAL FROM TRIAL"
* $NCIT#C218584  "ICH M11 Protocol Section 7.1 Discontinuation of Trial Intervention for Individual Participants"
* $NCIT#C218585  "ICH M11 Protocol Section 7.1.1 Permanent Discontinuation of Trial Intervention"
* $NCIT#C218586  "ICH M11 Protocol Section 7.1.2 Temporary Discontinuation of Trial Intervention"
* $NCIT#C218587  "ICH M11 Protocol Section 7.1.3 Rechallenge"
* $NCIT#C218588  "ICH M11 Protocol Section 7.2 Participant Discontinuation or Withdrawal from the Trial"
* $NCIT#C218589  "ICH M11 Protocol Section 7.3 Management of Loss to Follow-Up"
*/

      
* section[+]
  * title = "Section 7.1.1"
  * code = $NCIT#C218585  "ICH M11 Protocol Section 7.1.1 Permanent Discontinuation of Trial Intervention"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218764}}
        </div>
      """
      
      
* section[+]
  * title = "Section 7.1.2"
  * code = $NCIT#C218586  "ICH M11 Protocol Section 7.1.2 Temporary Discontinuation of Trial Intervention"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218765}}
        </div>
      """
      
* section[+]
  * title = "Section 7.1.3"
  * code = $NCIT#C218587  "ICH M11 Protocol Section 7.1.3 Rechallenge"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218766}}
        </div>
      """
      
* section[+]
  * title = "Section 7.2"
  * code = $NCIT#C218588  "ICH M11 Protocol Section 7.2 Participant Discontinuation or Withdrawal from the Trial"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218767}}
        </div>
      """
      
* section[+]
  * title = "Section 7.3"
  * code = $NCIT#C218589  "ICH M11 Protocol Section 7.3 Management of Loss to Follow-Up"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218768}}
        </div>
      """
