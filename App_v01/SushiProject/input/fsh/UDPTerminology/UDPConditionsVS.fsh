// UDPConditionsVS.fsh
// Value set for field C112038 - Population Diagnosis or Condition
// Contains representative SNOMED CT and MedDRA codes in a single value set.
// This is a DEMO / STARTER value set — extend with the conditions relevant to each trial.
// In practice, condition coding for a specific protocol will be trial-specific.
// Note: MedDRA licences restrict redistribution; confirm licence compliance before publishing.

ValueSet: UDPConditionVS
Id: udp-condition-vs
Title: "UDP Population Condition Value Set"
Description: """SNOMED CT and MedDRA Preferred Term codes for the Population Diagnosis or Condition field (C112038) of the ICH M11 protocol template.
This is a starter set covering common therapeutic areas. Extend or replace with the codes appropriate to the specific trial."""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 0
* ^experimental = true
* ^status = #draft
* ^publisher = "UDP"

// --- SNOMED CT ---

// Oncology
* $SCT#363346000  "Malignant neoplastic disease (SCT)"
* $SCT#254837009  "Malignant neoplasm of breast (SCT)"
* $SCT#93880001   "Primary malignant neoplasm of lung (SCT)"
* $SCT#363418001  "Malignant tumour of pancreas (SCT)"
* $SCT#363449006  "Malignant tumour of prostate (SCT)"
* $SCT#94381002   "Non-Hodgkin lymphoma (SCT)"

// Cardiovascular
* $SCT#53741008   "Coronary arteriosclerosis (SCT)"
* $SCT#44054006   "Diabetes mellitus type 2 (SCT)"
* $SCT#38341003   "Hypertensive disorder (SCT)"
* $SCT#84114007   "Heart failure (SCT)"
* $SCT#49436004   "Atrial fibrillation (SCT)"

// Respiratory
* $SCT#195967001  "Asthma (SCT)"
* $SCT#13645005   "Chronic obstructive lung disease (SCT)"

// Neurological / Psychiatric
* $SCT#49049000   "Parkinson disease (SCT)"
* $SCT#26929004   "Alzheimer disease (SCT)"
* $SCT#35489007   "Depressive disorder (SCT)"
* $SCT#69322001   "Psychotic disorder (SCT)"

// Inflammatory / Autoimmune
* $SCT#9014002    "Psoriasis (SCT)"
* $SCT#56246009   "Plaque psoriasis (SCT)"
* $SCT#69896004   "Rheumatoid arthritis (SCT)"
* $SCT#24526004   "Inflammatory bowel disease (SCT)"
* $SCT#34000006   "Crohn disease (SCT)"
* $SCT#64766004   "Ulcerative colitis (SCT)"

// Infectious Disease
* $SCT#86406008   "Human immunodeficiency virus infection (SCT)"
* $SCT#40122008   "Pneumonia (SCT)"

// --- MedDRA ---

// Oncology
* $MedDRA#10028997 "Neoplasm malignant (MedDRA)"
* $MedDRA#10006187 "Breast cancer (MedDRA)"
* $MedDRA#10025040 "Lung neoplasm malignant (MedDRA)"
* $MedDRA#10033605 "Pancreatic carcinoma (MedDRA)"
* $MedDRA#10060862 "Prostate cancer (MedDRA)"
* $MedDRA#10029547 "Non-Hodgkin's lymphoma (MedDRA)"

// Cardiovascular
* $MedDRA#10011078 "Coronary artery disease (MedDRA)"
* $MedDRA#10012601 "Diabetes mellitus type 2 (MedDRA)"
* $MedDRA#10020772 "Hypertension (MedDRA)"
* $MedDRA#10019280 "Heart failure (MedDRA)"
* $MedDRA#10003658 "Atrial fibrillation (MedDRA)"

// Respiratory
* $MedDRA#10003553 "Asthma (MedDRA)"
* $MedDRA#10009033 "Chronic obstructive pulmonary disease (MedDRA)"

// Neurological / Psychiatric
* $MedDRA#10061536 "Parkinson's disease (MedDRA)"
* $MedDRA#10001896 "Alzheimer's disease (MedDRA)"
* $MedDRA#10012378 "Depression (MedDRA)"
* $MedDRA#10037175 "Psychotic disorder (MedDRA)"

// Inflammatory / Autoimmune
* $MedDRA#10037153 "Psoriasis (MedDRA)"
* $MedDRA#10034461 "Plaque psoriasis (MedDRA)"
* $MedDRA#10039073 "Rheumatoid arthritis (MedDRA)"
* $MedDRA#10021972 "Inflammatory bowel disease (MedDRA)"
* $MedDRA#10011401 "Crohn's disease (MedDRA)"
* $MedDRA#10045365 "Ulcerative colitis (MedDRA)"

// Infectious Disease
* $MedDRA#10020161 "HIV infection (MedDRA)"
* $MedDRA#10035664 "Pneumonia (MedDRA)"
