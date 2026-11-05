// M11Additional.fsh
// Value sets used in the UDP app that do not yet have their own FSH file.

//-----------------------------------------------------
// Code List C217048
// ICH OID (to be confirmed)
ValueSet: M11TimeUnitVS
Id: m11-time-unit-vs
Title: "ICH M11 Time Unit Value Set Terminology"
Description: """Terminology associated with the Time Unit value set codelist of the ICH M11 protocol template.
This is ICH M11 Value Set C217048 drawn from the NCI Thesaurus and represented here in FHIR format.
Used to express the unit for age limits, intervention duration and participation duration."""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 0
* ^experimental = false
* ^status = #active
* ^publisher = "ICH M11"

* $NCIT#C25529 "Hours"
* $NCIT#C25301 "Days"
* $NCIT#C29844 "Weeks"
* $NCIT#C29846 "Months"
* $NCIT#C29848 "Years"

//-----------------------------------------------------
// Code List C217285: ICH OID 2.16.840.1.113883.3.989.2.3.1.8
// Moved from ext-m11-intervention.fsh
ValueSet: M11UseCodeVS
Id: m11-use-code-vs
Title: "M11 Use Code Elements Value Set using NCIT codes to create a FHIR value set."
Description: """Use Code elements for describing trial interventions. This is an M11 specific value set.
Code List C217285: ICH OID 2.16.840.1.113883.3.989.2.3.1.8"""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 2
* ^experimental = false
* ^status = #active
* ^publisher = "ICH M11"

* $NCIT#C41161  "Experimental Intervention"
* $NCIT#C753    "Placebo"
* $NCIT#C165835 "Rescue Medicine"
* $NCIT#C165822 "Background Treatment"
* $NCIT#C158128 "Challenge Agent"
* $NCIT#C18020  "Diagnostic"
* $NCIT#C207614 "Additional Required Treatment"

//-----------------------------------------------------
// Code List C217286: ICH OID 2.16.840.1.113883.3.989.2.3.1.10
// Moved from ext-m11-intervention.fsh
ValueSet: M11ImpNimpTypeVS
Id: m11-imp-nimp-type-vs
Title: "M11 imp-nimp Elements Value Set using NCIT codes to create a FHIR value set."
Description: """imp-nimp elements for describing trial interventions. This is an M11 specific value set.
Code List C217286: ICH OID 2.16.840.1.113883.3.989.2.3.1.10"""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 2
* ^experimental = false
* ^status = #active
* ^publisher = "ICH M11"

* $NCIT#C202579 "IMP"
* $NCIT#C156473 "NIMP"

//-----------------------------------------------------
// Code List C217052: ICH OID 2.16.840.1.113883.3.989.2.3.1.9
// Moved from ext-m11-intervention.fsh
ValueSet: M11SourceCodeVS
Id: m11-source-code-vs
Title: "M11 Source Code Elements Value Set using NCIT codes to create a FHIR value set."
Description: """Source Code elements for describing trial interventions. This is an M11 specific value set.
Code List C217052: ICH OID 2.16.840.1.113883.3.989.2.3.1.9"""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 2
* ^experimental = false
* ^status = #active
* ^publisher = "ICH M11"

* $NCIT#C215659 "Centrally Sourced"
* $NCIT#C215660 "Locally Sourced"

//-----------------------------------------------------
// Code List C217287: ICH OID 2.16.840.1.113883.3.989.2.3.1.22
// Moved from ext-m11-reporting.fsh
ValueSet: M11EventTypeVS
Id: m11-event-type-vs
Title: "M11 Event Type Elements Value Set using NCIT codes to create a FHIR value set."
Description: """Event Type elements for describing trial reportings. This is an M11 specific value set.
Code List C217287: ICH OID 2.16.840.1.113883.3.989.2.3.1.22"""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 2
* ^experimental = false
* ^status = #active
* ^publisher = "ICH M11"

* $NCIT#C41331  "Adverse Event"
* $NCIT#C41335  "Serious Adverse Event"
* $NCIT#C218508 "Trial Intervention Complaint"
* $NCIT#C222331 "Drug/Device Combination Product Complaint"
* $NCIT#C25742  "Pregnancy Event"
* $NCIT#C218510 "Lactation Event"
* $NCIT#C218511 "Post-Partum Event"
* $NCIT#C218512 "Reportable Adverse Event of Special Interest"
* $NCIT#C218513 "Not Reportable Adverse Event of Special Interest"
* $NCIT#C17649  "Other"

//-----------------------------------------------------
// Dose Form and Route: EDQM/IDMP-coded, not NCI — placeholder lists
// Replace $IDMP_DUMMY codes with real EDQM codes when available.

ValueSet: M11DoseFormVS
Id: m11-dose-form-vs
Title: "ICH M11 Pharmaceutical Dose Form Value Set"
Description: """Pharmaceutical dose form of the investigational product.
This value set uses EDQM/IDMP coding rather than NCI codes. The entries below are placeholders — replace with real EDQM standard term codes."""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 0
* ^experimental = false
* ^status = #active
* ^publisher = "ICH M11"

* $IDMP_DUMMY#f1 "tablet"
* $IDMP_DUMMY#f2 "capsule"
* $IDMP_DUMMY#f3 "solution for injection"

ValueSet: M11RouteVS
Id: m11-route-vs
Title: "ICH M11 Route of Administration Value Set"
Description: """Route of administration of the investigational product.
This value set uses EDQM/IDMP coding rather than NCI codes. The entries below are placeholders — replace with real EDQM standard term codes."""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 0
* ^experimental = false
* ^status = #active
* ^publisher = "ICH M11"

* $IDMP_DUMMY#R1 "oral"
* $IDMP_DUMMY#R2 "subcutaneous injection"
* $IDMP_DUMMY#R3 "topical"

//--------------------------------------------------------------------------------------
//
ValueSet: M11InterventionTypeVS
Id: m11-intervention-type-vs
Title: "M11 Intervention Type Elements Value Set using NCIT codes to create a FHIR value set."
Description: """Intervention Type elements for describing trial interventions. This is an M11 specific value set.
- Code List C217284: ICH OID 2.16.840.1.113883.3.989.2.3.1.7"""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 2
* ^experimental = false
* ^status = #active
* ^publisher = "ICH M11" 

* $NCIT#C1909 "Drug" 
* $NCIT#C16830 "Medical Device" 
* $NCIT#C307 "Biologic" 
* $NCIT#C923 "Vaccine" 
* $NCIT#C218507 "Non Surgical Procedure" 
* $NCIT#C15329 "Surgery" 
* $NCIT#C15313 "Radiation" 
* $NCIT#C15184 "Behavioral" 
* $NCIT#C15238 "Genetic" 
* $NCIT#C1505 "Dietary Supplement" 
* $NCIT#C54696 "Combination Product" 
* $NCIT#C18020 "Diagnostic" 

//--------------------------------------------------------------------------------------
//
ValueSet: M11ArmTypeVS
Id: m11-arm-type-vs
Title: "M11 Arm Type Elements Value Set using NCIT codes to create a FHIR value set."
Description: """Arm Type elements for describing trial interventions. This is an M11 specific value set.
Code List C217283: ICH OID 2.16.840.1.113883.3.989.2.3.1.15"""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 2
* ^experimental = false
* ^status = #active
* ^publisher = "ICH M11"

* $NCIT#C174267 "Active Comparator Arm"
* $NCIT#C174226 "Control Arm"
* $NCIT#C174266 "Experimental Arm"
* $NCIT#C174270 "No Intervention Arm"
* $NCIT#C174268 "Placebo Comparator Arm"
* $NCIT#C174269 "Sham Comparator Arm"

//--------------------------------------------------------------------------------------
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

* $NCIT#C218486	"First Protocol Amendment"
* $NCIT#C218485	"Protocol Not Amended"
* $NCIT#C218488	"Protocol Previously Amended See Summary of Changes Before the Table of Contents"
* $NCIT#C218487	"Protocol Previously Amended, Details Presented"