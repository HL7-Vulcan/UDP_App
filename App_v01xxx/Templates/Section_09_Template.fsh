Instance: Narrative-Composition-M11Section09
InstanceOf: m11-research-study-narratives
Title: "Example Narrative Single Composition with a Section for Each M11 Section"
Usage: #example
Description: """Example Narrative Single Composition with a contained section for e part of M11 Section 09.
"""

* status = #final
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"

/* 
* $NCIT#C218608  "ICH M11 Protocol Section 9 ADVERSE EVENTS, SERIOUS ADVERSE EVENTS, PRODUCT COMPLAINTS, PREGNANCY AND POSTPARTUM INFORMATION, AND SPECIAL SAFETY SITUATIONS"
* $NCIT#C218609  "ICH M11 Protocol Section 9.1 Definitions"
* $NCIT#C218610  "ICH M11 Protocol Section 9.1.1 Definitions of Adverse Events"
* $NCIT#C218611  "ICH M11 Protocol Section 9.1.2 Definitions of Serious Adverse Events"
* $NCIT#C218612  "ICH M11 Protocol Section 9.1.3 Definitions of Product Complaints"
* $NCIT#C218613  "ICH M11 Protocol Section 9.1.3.1 Definitions of Medical Device Product Complaints"
* $NCIT#C218614  "ICH M11 Protocol Section 9.2 Timing and Procedures for Collection and Reporting"
* $NCIT#C218615  "ICH M11 Protocol Section 9.2.1 Timing"
* $NCIT#C218616  "ICH M11 Protocol Section 9.2.2 Collection Procedures"

* $UDP_Term#013  "ICH M11 Protocol Section 9.2.2.1 Collection Procedures: Identification"
* $UDP_Term#014  "ICH M11 Protocol Section 9.2.2.2 Collection Procedures: Severity"
* $UDP_Term#015  "ICH M11 Protocol Section 9.2.2.3 Collection Procedures: Causality"
* $UDP_Term#016  "ICH M11 Protocol Section 9.2.2.4 Collection Procedures: Recording"
* $UDP_Term#017  "ICH M11 Protocol Section 9.2.2.5 Collection Procedures: Follow-up"

* $NCIT#C218617  "ICH M11 Protocol Section 9.2.3 Reporting"
* $NCIT#C218618  "ICH M11 Protocol Section 9.2.3.1 Regulatory Reporting Requirements"
* $NCIT#C218619  "ICH M11 Protocol Section 9.2.4 Adverse Events of Special Interest"
* $NCIT#C218620  "ICH M11 Protocol Section 9.2.5 Disease-related Events or Outcomes Not Qualifying as AEs or SAEs"
* $NCIT#C218621  "ICH M11 Protocol Section 9.3 Pregnancy and Postpartum Information"
* $NCIT#C218622  "ICH M11 Protocol Section 9.3.1 Participants Who Become Pregnant During the Trial"
* $NCIT#C218623  "ICH M11 Protocol Section 9.3.2 Participants Whose Partners Become Pregnant During the Trial"
* $NCIT#C218624  "ICH M11 Protocol Section 9.4 Special Safety Situations"


*/


* section[+]
  * title = "Section 9.1.1"
  * code = $NCIT#C218610  "ICH M11 Protocol Section 9.1.1 Definitions of Adverse Events"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218476}}
        </div>
      """



* section[+]
  * title = "Section 9.1.2"
  * code = $NCIT#C218611  "ICH M11 Protocol Section 9.1.2 Definitions of Serious Adverse Events"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218781}}
        </div>
      """


* section[+]
  * title = "Section 9.1.3"
  * code = $NCIT#C218612  "ICH M11 Protocol Section 9.1.3 Definitions of Product Complaints"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218782}}
        </div>
      """


* section[+]
  * title = "Section 9.1.3.1"
  * code = $NCIT#C218613  "ICH M11 Protocol Section 9.1.3.1 Definitions of Medical Device Product Complaints"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218783}}
        </div>
      """


* section[+]
  * title = "Section 9.2"
  * code = $NCIT#C218614  "ICH M11 Protocol Section 9.2 Timing and Procedures for Collection and Reporting"
{{REPEAT  * entry[+] = Reference({{TABLE_ROW_X}})}} 

* section[+]
  * title = "Section 9.2.1"
  * code = $NCIT#C218615  "ICH M11 Protocol Section 9.2.1 Timing"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218791}}
        </div>
      """


* section[+]
  * title = "Section 9.2.2.1"
  * code = $UDP_Term#013  "ICH M11 Protocol Section 9.2.2.1 Collection Procedures: Identification"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218792}}
        </div>
      """


* section[+]
  * title = "Section 9.2.2.2"
  * code = $UDP_Term#014  "ICH M11 Protocol Section 9.2.2.2 Collection Procedures: Severity"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C25676}}
        </div>
      """


* section[+]
  * title = "Section 9.2.2.3"
  * code = $UDP_Term#015  "ICH M11 Protocol Section 9.2.2.3 Collection Procedures: Causality"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C82552}}
        </div>
      """


* section[+]
  * title = "Section 9.2.2.4"
  * code = $UDP_Term#016  "ICH M11 Protocol Section 9.2.2.4 Collection Procedures: Recording"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{Cxxx}}
        </div>
      """


* section[+]
  * title = "Section 9.2.2.5"
  * code = $UDP_Term#017  "ICH M11 Protocol Section 9.2.2.5 Collection Procedures: Follow-up"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218793}}
        </div>
      """

* section[+]
  * title = "Section 9.2.3"
  * code = $NCIT#C218617  "ICH M11 Protocol Section 9.2.3 Reporting"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218795}}
        </div>
      """


* section[+]
  * title = "Section 9.2.3.1"
  * code = $NCIT#C218618  "ICH M11 Protocol Section 9.2.3.1 Regulatory Reporting Requirements"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218796}}
        </div>
      """


* section[+]
  * title = "Section 9.2.4"
  * code = $NCIT#C218619  "ICH M11 Protocol Section 9.2.4 Adverse Events of Special Interest"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C217358}}
        </div>
      """


* section[+]
  * title = "Section 9.2.5"
  * code = $NCIT#C218620  "ICH M11 Protocol Section 9.2.5 Disease-related Events or Outcomes Not Qualifying as AEs or SAEs"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{CxC21877xx}}
        </div>
      """


* section[+]
  * title = "Section 9.3.1"
  * code = $NCIT#C218622  "ICH M11 Protocol Section 9.3.1 Participants Who Become Pregnant During the Trial"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218798}}
        </div>
      """


* section[+]
  * title = "Section 9.3.2"
  * code = $NCIT#C218623  "ICH M11 Protocol Section 9.3.2 Participants Whose Partners Become Pregnant During the Trial"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218799}}
        </div>
      """


* section[+]
  * title = "Section 9.4"
  * code = $NCIT#C218624  "ICH M11 Protocol Section 9.4 Special Safety Situations"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218800}}
        </div>
      """
