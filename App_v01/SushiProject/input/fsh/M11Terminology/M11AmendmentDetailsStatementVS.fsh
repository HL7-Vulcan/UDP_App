//ValueSet: C217274
ValueSet: M11AmendmentDetailsStatementVS
Id: m11-amendment-details-statement-vs
Title: "ICH M11 Amendment Details Statement Value Set Terminology"
Description: """Terminology associated with whether or not a protocol has been amended.
This is ICH M11 Value Set C217274 drawn from the NCI Thesaurus and represented here in FHIR format."""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 0
* ^experimental = false
* ^status = #active
//* ^caseSensitive = false
* ^publisher = "ICH M11"

* $NCIT#C218485 "This protocol has not been amended."
* $NCIT#C218486 "This is the first protocol amendment." 
* $NCIT#C218487 "This protocol has been amended previously. Details of prior amendments are presented in Section 12.3 Prior Protocol Amendment(s)."


