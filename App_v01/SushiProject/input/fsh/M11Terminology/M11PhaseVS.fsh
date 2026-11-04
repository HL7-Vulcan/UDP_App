//ValueSet: C217045
//Id: C217045-vs
ValueSet: M11PhaseVS
Id: m11-phase-vs
Title: "ICH M11 Trial Phase Value Set Terminology"
Description: """Terminology associated with the trial phase value set codelist of the ICH M11 protocol template.
This is ICH M11 Value Set C217045 drawn from the NCI Thesaurus and represented here in FHIR format."""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 0
* ^experimental = false
* ^status = #active
//* ^caseSensitive = false
* ^publisher = "ICH M11"
* $NCIT#C54721 "Early Phase 1"
* $NCIT#C15600 "Phase 1"
* $NCIT#C15693 "Phase 1/Phase 2"
* $NCIT#C198366 "Phase 1/Phase 2/Phase 3"
* $NCIT#C198367 "Phase 1/Phase 3"
* $NCIT#C15601 "Phase 2"
* $NCIT#C15694 "Phase 2/Phase 3"
* $NCIT#C217024 "Phase 2/Phase 3/Phase 4"
* $NCIT#C15602 "Phase 3"
* $NCIT#C217025 "Phase 3/Phase 4"
* $NCIT#C15603 "Phase 4"
