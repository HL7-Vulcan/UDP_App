Instance: Narrative-Composition-M11Section04
InstanceOf: m11-research-study-narratives
Title: "Example Narrative Single Composition with a Section for Each M11 Section"
Usage: #example
Description: """Example Narrative Single Composition with a contained section for e part of M11 Section 04.
"""

* status = #final
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"

/* 
* $NCIT#C218533  "ICH M11 Protocol Section 4 TRIAL DESIGN"
* $NCIT#C218534  "ICH M11 Protocol Section 4.1 Description of Trial Design"
* $UDP_Term#004  "ICH M11 Protocol Section 4.1.0.1 Overall Description of Trial Design and Description of Intervention Model"
* $UDP_Term#005  "ICH M11 Protocol Section 4.1.0.2 Description of Trial Duration"
* $UDP_Term#006  "ICH M11 Protocol Section 4.1.0.3 Description of Method of Assignment to Trial Intervention"
* $UDP_Term#007  "ICH M11 Protocol Section 4.1.0.4 Description of Level and Method of Blinding"
* $UDP_Term#008  "ICH M11 Protocol Section 4.1.0.5 Additional Description of Trial Design"
* $NCIT#C218535  "ICH M11 Protocol Section 4.1.1 Stakeholder Input into Design"
* $NCIT#C218536  "ICH M11 Protocol Section 4.2 Rationale for Trial Design"
* $NCIT#C218537  "ICH M11 Protocol Section 4.2.1 Rationale for Estimand(s)"
* $NCIT#C218538  "ICH M11 Protocol Section 4.2.2 Rationale for Intervention Model"
* $NCIT#C218539  "ICH M11 Protocol Section 4.2.3 Rationale for Control Type"
* $NCIT#C218540  "ICH M11 Protocol Section 4.2.4 Rationale for Trial Duration"
* $NCIT#C218541  "ICH M11 Protocol Section 4.2.5 Rationale for Adaptive or Novel Trial Design"
* $NCIT#C218542  "ICH M11 Protocol Section 4.2.6 Rationale for Interim Analysis"
* $NCIT#C218543  "ICH M11 Protocol Section 4.2.7 Rationale for Other Trial Design Aspects"
* $NCIT#C218544  "ICH M11 Protocol Section 4.3 Trial Stopping Rules"
* $NCIT#C218545  "ICH M11 Protocol Section 4.4 Start of Trial and End of Trial"
* $UDP_Term#020  "ICH M11 Protocol Section 4.4.1 Start of Trial"
* $UDP_Term#021  "ICH M11 Protocol Section 4.4.2 End of Trial"
* $NCIT#C218546  "ICH M11 Protocol Section 4.5 Access to Trial Intervention After End of Trial"
*/

      
* section[+]
  * title = "Section 4.1"
  * code = $NCIT#C218534  "ICH M11 Protocol Section 4.1 Description of Trial Design"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C147139}}
        </div>
      """

* section[+]
  * title = "Section 4.1.0.1"
  * code = $UDP_Term#004  "ICH M11 Protocol Section 4.1.0.1 Overall Description of Trial Design and Description of Intervention Model"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218726}}
        </div>
      """ 

* section[+]
  * title = "Section 4.1.0.2"
  * code = $UDP_Term#005  "ICH M11 Protocol Section 4.1.0.2 Description of Trial Duration"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218726}}
        </div>
      """

* section[+]
  * title = "Section 4.1.0.3"
  * code = $UDP_Term#006  "ICH M11 Protocol Section 1.0.34.1.0.3 Description of Method of Assignment to Trial Intervention"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C219658}}
        </div>
      """ 

* section[+]
  * title = "Section 4.1.0.4"
  * code = $UDP_Term#007  "ICH M11 Protocol Section 4.1.0.4 Description of Level and Method of Blinding"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218727}}
        </div>
      """ 

* section[+]
  * title = "Section 4.1.0.5"
  * code = $UDP_Term#008  "ICH M11 Protocol Section 4.1.0.5 Additional Description of Trial Design"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218728}}
        </div>
      """ 

* section[+]
  * title = "Section 4.1.1"
  * code = $NCIT#C218535  "ICH M11 Protocol Section 4.1.1 Stakeholder Input into Design"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218729}}
        </div>
      """ 

* section[+]
  * title = "Section 4.2"
  * code = $NCIT#C218536  "ICH M11 Protocol Section 4.2 Rationale for Trial Design"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C142705}}
        </div>
      """ 

* section[+]
  * title = "Section 4.2.1"
  * code = $NCIT#C218537  "ICH M11 Protocol Section 4.2.1 Rationale for Estimand(s)"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218730}}
        </div>
      """ 

* section[+]
  * title = "Section 4.2.2"
  * code = $NCIT#C218538  "ICH M11 Protocol Section 4.2.2 Rationale for Intervention Model"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C215629}}
        </div>
      """ 

* section[+]
  * title = "Section 4.2.3"
  * code = $NCIT#C218539  "ICH M11 Protocol Section 4.2.3 Rationale for Control Type"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218731}}
        </div>
      """ 

* section[+]
  * title = "Section 4.2.4"
  * code = $NCIT#C218540  "ICH M11 Protocol Section 4.2.4 Rationale for Trial Duration"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218732}}
        </div>
      """ 

* section[+]
  * title = "Section 4.2.5"
  * code = $NCIT#C218541  "ICH M11 Protocol Section 4.2.5 Rationale for Adaptive or Novel Trial Design"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218733}}
        </div>
      """ 

* section[+]
  * title = "Section 4.2.6."
  * code = $NCIT#C218542  "ICH M11 Protocol Section 4.2.6 Rationale for Interim Analysis"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218734}}
        </div>
      """ 

* section[+]
  * title = "Section 4.2.7"
  * code = $NCIT#C218543  "ICH M11 Protocol Section 4.2.7 Rationale for Other Trial Design Aspects"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218735}}
        </div>
      """ 

* section[+]
  * title = "Section 4.3"
  * code = $NCIT#C218544  "ICH M11 Protocol Section 4.3 Trial Stopping Rules"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C142698}}
        </div>
      """ 

* section[+]
  * title = "Section 4.4.1"
  * code = $UDP_Term#020  "ICH M11 Protocol Section 4.4.1 Start of Trial"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218736}}
        </div>
      """ 

* section[+]
  * title = "Section 4.4.2"
  * code = $UDP_Term#021  "ICH M11 Protocol Section 4.4.2 End of Trial"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218737}}
        </div>
      """ 
