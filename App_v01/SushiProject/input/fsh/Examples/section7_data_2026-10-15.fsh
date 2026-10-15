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
Investigational product will be permanently discontinued if a participant experiences a Grade 4 adverse event, or a Grade 3 adverse event that does not resolve to Grade 1 or baseline within 14 days.
        </div>
      """
      
      
* section[+]
  * title = "Section 7.1.2"
  * code = $NCIT#C218586  "ICH M11 Protocol Section 7.1.2 Temporary Discontinuation of Trial Intervention"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Investigational product may be temporarily discontinued at the investigator's discretion for management of adverse events. The maximum permitted interruption is 28 days.
        </div>
      """
      
* section[+]
  * title = "Section 7.1.3"
  * code = $NCIT#C218587  "ICH M11 Protocol Section 7.1.3 Rechallenge"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Rechallenge with the investigational product after temporary discontinuation is permitted at the discretion of the investigator, provided the adverse event has resolved to Grade 1 or baseline.
        </div>
      """
      
* section[+]
  * title = "Section 7.2"
  * code = $NCIT#C218588  "ICH M11 Protocol Section 7.2 Participant Discontinuation or Withdrawal from the Trial"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Participants may withdraw from the trial at any time without prejudice. Withdrawal may also be initiated by the investigator if continued participation is not in the participant's best interest.
        </div>
      """
      
* section[+]
  * title = "Section 7.3"
  * code = $NCIT#C218589  "ICH M11 Protocol Section 7.3 Management of Loss to Follow-Up"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Every reasonable effort will be made to contact participants who are lost to follow-up. At least three attempts using different methods of contact must be documented before a participant is classified as lost to follow-up.
        </div>
      """
