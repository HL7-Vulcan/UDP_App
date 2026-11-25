Extension: M11_ResearchStudy
Id: m11-research-study
Description: "Additions for M11 not otherwise dealt with"
Title:  "M11 Research Study Extension"
Context: M11_ResearchStudyProfile
* insert rs-copyright-structure
* ^extension[$ext-fmm].valueInteger = 2
* ^extension[$ext-wg].valueCode = #brr
* ^status = #active

* value[x] 0..0
* . ^short = "Additional concepts for M11"
* . ^definition = "Signature and date for approval."
* . ^comment = "Additional concepts required by M11 that have no other obvious home."

* extension contains
  originalProtocol 1..1 MS and
  versionDate 1..1 and
  m11-protocol-differences named differences 0..*

* extension[originalProtocol].value[x] only CodeableConcept
* extension[originalProtocol].value[x] from m11-yes-no-vs //m11-amendment-details-statement-vs
  * ^short = "Original Protocol Indicator"
  * ^definition = "An indication as to whether the protocol document reflects the original version of the protocol.."

* extension[versionDate].value[x] only date
  * ^short = "Protocol version date"
  * ^definition = "The date on which the document is versioned."
  * ^comment = "The date on ResearchStudy is the date the resource last significantly changed - not the same as the protocol version date"

//-------------------------------------------------------------------------------------
//
Extension: M11_ProtocolDifferences
Id: m11-protocol-differences
Description: "Regional differencesin protocol"
Title:  "M11 Research Study Regional Differences"
Context: M11_ResearchStudyProfile
* insert rs-copyright-structure
* ^extension[$ext-fmm].valueInteger = 2
* ^extension[$ext-wg].valueCode = #brr
* ^status = #active

* value[x] 0..0

* extension contains
  country 1..1 MS and
  requirements 0..1 and
  clarification 0..1

* extension[country].value[x] only CodeableConcept 
* extension[country].value[x] from M11CountryRegionVS (extensible)

* extension[requirements].value[x] only string
* extension[clarification].value[x] only string