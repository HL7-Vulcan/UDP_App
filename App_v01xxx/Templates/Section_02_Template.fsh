Instance: Narrative-Composition-M11Section02
InstanceOf: m11-research-study-narratives
Title: "Example Narrative Single Composition with a Section for Each M11 Section"
Usage: #example
Description: """Example Narrative Single Composition with a contained section for e part of M11 Section 02.
"""

* status = #final
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"
* section[+]
  * title = "Section 2.1"
  * code = $NCIT#C218521  "ICH M11 Protocol Section 2.1 Purpose of Trial"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
              {{C146997}}
        </div>
      """
* section[+]
  * title = "Section 2.2.1.1"
  /// TODO Check this isn't in NCIT
  * code = $UDP_Term#001  "ICH M11 Protocol Section 2.2.1.1 Risk Summary and Mitigation Strategy - Trial Intervention"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
              {{C218721}}
    </div>
    """
* section[+]
  * title = "Section 2.2.1.2"
  /// TODO Check this isn't in NCIT
  * code = $UDP_Term#002  "ICH M11 Protocol Section 2.2.1.2 Risk Summary and Mitigation Strategy - Trial Procedures"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
              {{C218722}}
    </div>
    """
* section[+]
  * title = "Section 2.2.1.3"
  /// TODO Check this isn't in NCIT
  * code = $UDP_Term#003  "ICH M11 Protocol Section 2.2.1.3 Risk Summary and Mitigation Strategy - Other"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
              {{C218723}}
    """

* section[+]
  * title = "Section 2.2.2"
  * code = $NCIT#C218524  "ICH M11 Protocol Section 2.2.2 Benefit Summary"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
              {{C218724}}
    </div>
    """

* section[+]
  * title = "Section 2.2.3"
  * code = $NCIT#C218525  "ICH M11 Protocol Section 2.2.3 Overall Risk-Benefit Assessment"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
              {{C218725}}
    </div>
    """