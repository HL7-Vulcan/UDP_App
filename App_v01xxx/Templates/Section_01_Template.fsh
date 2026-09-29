Instance: Narrative-Composition-M11Section01
InstanceOf: m11-research-study-narratives
Title: "Example Narrative Single Composition with a Section for Each M11 Section"
Usage: #example
Description: """Example Narrative Single Composition with a contained section for e part of M11 Section 01.
"""

* status = #final
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"

/* 
* $NCIT#C218514  "ICH M11 Protocol Section 1 PROTOCOL SUMMARY"
* $NCIT#C218515  "ICH M11 Protocol Section 1.1 Protocol Synopsis"
* $NCIT#C218516  "ICH M11 Protocol Section 1.1.1 Primary and Secondary Objectives and Estimands"
* $NCIT#C218517  "ICH M11 Protocol Section 1.1.2 Overall Design"
* $NCIT#C218518  "ICH M11 Protocol Section 1.2 Trial Schema"
* $UDP_Term#018  "ICH M11 Protocol Section 1.2.1 Trial Schema Description"
* $UDP_Term#019  "ICH M11 Protocol Section 1.2.2 Trial Schema Notes"
* $NCIT#C218519  "ICH M11 Protocol Section 1.3 Schedule of Activities"
*/

      
* section[+]
  * title = "Section 1.1.1"
  * code = $NCIT#C218516  "ICH M11 Protocol Section 1.1.1 Primary and Secondary Objectives and Estimands"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218839}}
        </div>
      """
    
 
* section[+]
  * title = "Section 1.1.2"
  * code = $NCIT#C218517  "ICH M11 Protocol Section 1.1.2 Overall Design"
  // * text.status = #additional
  // * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  //             <p>Hic textus tantummodo locum tenens est. Nullum verum sensum habet praeter spatium in pagina vel velo implere. Utile tamen est ad rem illustrandam. Huiusmodi ineptiae plerumque cum \"lorem ipsum\" incipiunt, sed ita translatae non sunt textus laetus.</p>
  //       </div>
  //     """
  * entry[+] = Reference({{TABLE_X}})

* section[+]
  * title = "Section 1.2"
  * code = $NCIT#C218518  "ICH M11 Protocol Section 1.2 Trial Schema"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218720}}
        </div>
      """
 

* section[+]
  * title = "Section 1.3"
  * code = $NCIT#C218519  "ICH M11 Protocol Section 1.3 Schedule of Activities"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C132349}}
        </div>
      """
  //* entry[+] = Reference(soa-01)     
