Instance: Narrative-Composition-M11Section05
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

/* 
* $NCIT#C218547  "ICH M11 Protocol Section 5 TRIAL POPULATION"
* $NCIT#C218548  "ICH M11 Protocol Section 5.1 Description of Trial Population and Rationale"
* $NCIT#C218549  "ICH M11 Protocol Section 5.2 Inclusion Criteria"
* $NCIT#C218550  "ICH M11 Protocol Section 5.3 Exclusion Criteria"
* $NCIT#C218551  "ICH M11 Protocol Section 5.4 Contraception"
* $NCIT#C218552  "ICH M11 Protocol Section 5.4.1 Definitions Related to Childbearing Potential"
* $NCIT#C218553  "ICH M11 Protocol Section 5.4.2 Contraception Requirements"
* $NCIT#C218554  "ICH M11 Protocol Section 5.5 Lifestyle Restrictions"
* $NCIT#C218555  "ICH M11 Protocol Section 5.5.1 Meals and Dietary Restrictions"
* $NCIT#C218556  "ICH M11 Protocol Section 5.5.2 Caffeine, Alcohol, Tobacco, and Other Restrictions"
* $NCIT#C218557  "ICH M11 Protocol Section 5.5.3 Physical Activity Restrictions"
* $NCIT#C218558  "ICH M11 Protocol Section 5.5.4 Other Activity Restrictions"
* $NCIT#C218559  "ICH M11 Protocol Section 5.6 Screen Failure and Rescreening"
* $UDP_Term#009  "ICH M11 Protocol Section 5.6.1 Screen Failure"
* $UDP_Term#010  "ICH M11 Protocol Section 5.6.2 Rescreening"
*/


* section[+]
  * title = "Section 5.1"
  * code = $NCIT#C218548  "ICH M11 Protocol Section 5.1 Description of Trial Population and Rationale"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
        {{C218739}}
        </div>
      """


// Patients aged 30-60 years with a body-mass index of 35 kg/m(2) or more and a history of type 2 diabetes lasting at least 5 years
* section[+]
  * title = "Section 5.2"
  * code = $NCIT#C218549  "ICH M11 Protocol Section 5.2 Inclusion Criteria"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C25532}}
        </div>
      """
 {{REPEAT * entry[+] = Reference(eligibility-group-{{TABLE_ROW_X}})}}
//  * extension[order].valueInteger = {{n}}

// * section[+]
//   * title = "Section 5.2"
//   * code = $NCIT#C218549  "ICH M11 Protocol Section 5.2 Inclusion Criteria"
//   * text.status = #additional
//   * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
// {{C25532}}
//         </div>
//       """
//   * entry[+] = Reference(eligibility-group-diabetes) 
//   * extension[order].valueInteger = 2


* section[+]
  * title = "Section 5.3"
  * code = $NCIT#C218550  "ICH M11 Protocol Section 5.3 Exclusion Criteria"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C25370}}
    </div>
    """

* section[+]
  * title = "Section 5.4.1"
  * code = $NCIT#C218552  "ICH M11 Protocol Section 5.4.1 Definitions Related to Childbearing Potential"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218740}}
    </div>
    """

* section[+]
  * title = "Section 5.4.2"
  * code = $NCIT#C218553  "ICH M11 Protocol Section 5.4.2 Contraception Requirements"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218741}}
    </div>
    """

* section[+]
  * title = "Section 5.5"
  * code = $NCIT#C218554  "ICH M11 Protocol Section 5.5 Lifestyle Restrictions"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218742}}
    </div>
    """

* section[+]
  * title = "Section 5.5.1"
  * code = $NCIT#C218555  "ICH M11 Protocol Section 5.5.1 Meals and Dietary Restrictions"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218743}}
    </div>
    """

* section[+]
  * title = "Section 5.5.2"
  * code = $NCIT#C218559  "ICH M11 Protocol Section 5.5.2 Caffeine, Alcohol, Tobacco, and Other Restrictions"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218744}}
    </div>
    """

* section[+]
  * title = "Section 5.5.3"
  * code = $NCIT#C218557  "ICH M11 Protocol Section 5.5.3 Physical Activity Restrictions"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218745}}
    </div>
    """

* section[+]
  * title = "Section 5.5.4"
  * code = $NCIT#C218558  "ICH M11 Protocol Section 5.5.4 Other Activity Restrictions"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218746}}
    </div>
    """



* section[+]
  * title = "Section 5.6.1"
  * code = $UDP_Term#009  "ICH M11 Protocol Section 5.6.1 Screen Failure"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C49628}}
    </div>
    """

* section[+]
  * title = "Section 5.6.2"
  * code = $UDP_Term#010  "ICH M11 Protocol Section 5.6.2 Rescreening"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C179373}}
    </div>
    """