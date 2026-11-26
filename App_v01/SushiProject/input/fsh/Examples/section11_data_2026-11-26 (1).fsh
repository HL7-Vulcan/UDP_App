Instance: Narrative-Composition-M11Section11
InstanceOf: m11-research-study-narratives
Title: "Example Narrative Single Composition with a Section for Each M11 Section"
Usage: #example
Description: """Example Narrative Single Composition with a contained section for e part of M11 Section 11.
"""

* status = #final
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"

/* 
* $NCIT#C218649  "ICH M11 Protocol Section 11 TRIAL OVERSIGHT AND OTHER GENERAL CONSIDERATIONS"
* $NCIT#C218650  "ICH M11 Protocol Section 11.1 Regulatory and Ethical Considerations"
* $NCIT#C218651  "ICH M11 Protocol Section 11.2 Trial Oversight"
* $NCIT#C218652  "ICH M11 Protocol Section 11.2.1 Investigator Responsibilities"
* $NCIT#C218653  "ICH M11 Protocol Section 11.2.2 Sponsor Responsibilities"
* $NCIT#C218654  "ICH M11 Protocol Section 11.3 Informed Consent Process"
11.3.0.1 Description of Informed Consent Process
11.3.0.2 Description of Assent Process
11.3.0.3 Description of Emergency Consent Process
* $NCIT#C218655  "ICH M11 Protocol Section 11.3.1 Informed Consent for Rescreening"
* $NCIT#C218656  "ICH M11 Protocol Section 11.3.2 Informed Consent for Use of Remaining Samples in Exploratory Research"
* $NCIT#C218657  "ICH M11 Protocol Section 11.4 Committees"
* $NCIT#C218658  "ICH M11 Protocol Section 11.5 Insurance and Indemnity"
* $NCIT#C218659  "ICH M11 Protocol Section 11.6 Risk-Based Quality Management"
* $NCIT#C218660  "ICH M11 Protocol Section 11.7 Data Governance"
* $NCIT#C218661  "ICH M11 Protocol Section 11.8 Data Protection"
* $NCIT#C218662  "ICH M11 Protocol Section 11.9 Source Records"
11.9.0.2 Investigator Expectations for Source Records
11.9.0.3 Trial Monitor Expectations for Source Records
11.9.0.4 Identification of Source Records
* $NCIT#C218663  "ICH M11 Protocol Section 11.10 Protocol Deviations"
* $NCIT#C218664  "ICH M11 Protocol Section 11.11 Early Site Closure"
11.11.0.1 Decision Rights
11.11.0.2 Investigator Criteria
11.11.0.3 Responsibilities
* $NCIT#C218665  "ICH M11 Protocol Section 11.12 Data Dissemination"
*/

      
* section[+]
  * title = "Section 11.1"
  * code = $NCIT#C218650  "ICH M11 Protocol Section 11.1 Regulatory and Ethical Considerations"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
This trial will be conducted in accordance with the ethical principles of the Declaration of Helsinki (2013), ICH E6(R2) Good Clinical Practice (GCP), and all applicable national and local regulatory requirements.

Approval from an independent ethics committee (IEC) or institutional review board (IRB) will be obtained for each participating site before the trial commences. Regulatory authority approval will be obtained in each country prior to the first participant being enrolled at sites in that country. Any subsequent substantial amendments to the protocol will be submitted to the IEC/IRB and regulatory authority as required.

Written informed consent will be obtained from all participants prior to any trial-specific screening procedures. Participants will be given adequate time to consider their participation and will have the opportunity to ask questions. The investigator will ensure that the participant understands the nature, purpose, risks, and benefits of participating in the trial before obtaining consent. Participants may withdraw consent at any time without consequence to their future medical care.

This trial involves Japanese participants and will be conducted in accordance with the Japanese Act on the Protection of Personal Information (APPI) and applicable pharmaceutical regulations of the Ministry of Health, Labour and Welfare (MHLW). All relevant local requirements for trial registration, patient information, and data reporting will be observed.

Participants will be informed of their right to access their personal data and to request correction of any inaccuracies. All trial data will be handled in accordance with applicable data protection legislation. Personal data will be stored securely and access will be restricted to authorised trial personnel.
        </div>
      """
      
* section[+]
  * title = "Section 11.2"
  * code = $NCIT#C218651  "ICH M11 Protocol Section 11.2 Trial Oversight"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 

        </div>
      """

* section[+]
  * title = "Section 11.2.1"
  * code = $NCIT#C218652  "ICH M11 Protocol Section 11.2.1 Investigator Responsibilities"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
The investigator is responsible for the overall conduct of the trial at the site. Responsibilities include ensuring that the protocol is followed, that all required data are recorded accurately, and that informed consent is obtained from each participant prior to enrolment. The investigator will supervise all trial-related activities delegated to qualified site staff and will promptly report any protocol deviations or adverse events to the sponsor.
        </div>
      """
      
* section[+]
  * title = "Section 11.2.2"
  * code = $NCIT#C218653  "ICH M11 Protocol Section 11.2.2 Sponsor Responsibilities"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
The sponsor is responsible for the design, implementation, management, and financing of the trial. Sponsor responsibilities include selecting qualified investigators, supplying investigational medicinal product, providing trial-specific training, ensuring regulatory submissions are complete, and overseeing the quality of trial conduct through a risk-based monitoring programme.
        </div>
      """
      
* section[+]
  * title = "Section 11.3"
  * code = $NCIT#C218654  "ICH M11 Protocol Section 11.3 Informed Consent Process"
  * section[+]
    * title = "Description of Informed Consent Process"
    * text.status = #additional    
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  Written informed consent will be obtained from each participant before any trial-specific procedures are performed. The investigator or a qualified designee will explain the nature, purpose, potential risks and benefits, and alternatives to participation. The participant will be given sufficient time to consider their decision and to ask questions. Consent will be documented using an IEC/IRB-approved informed consent form (ICF). A signed copy of the ICF will be provided to the participant and the original retained in the trial master file.
          </div>
        """
  * section[+]
    * title = "Description of Assent Process"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  
          </div>
        """
  * section[+]
    * title = "Description of Emergency Consent Process"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  
          </div>
        """

* section[+]
  * title = "Section 11.3.1"
  * code = $NCIT#C218655  "ICH M11 Protocol Section 11.3.1 Informed Consent for Rescreening"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 

        </div>
      """
      
* section[+]
  * title = "Section 11.3.2"
  * code = $NCIT#C218656  "ICH M11 Protocol Section 11.3.2 Informed Consent for Use of Remaining Samples in Exploratory Research"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 

        </div>
      """
      
* section[+]
  * title = "Section 11.4"
  * code = $NCIT#C218657  "ICH M11 Protocol Section 11.4 Committees"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
An independent Data Safety Monitoring Board (DSMB) will review accumulating safety and efficacy data at pre-specified intervals throughout the trial. The DSMB will operate under a charter that specifies membership, meeting frequency, decision rules and reporting procedures. The DSMB may recommend continuation, modification or early termination of the trial based on its reviews.
        </div>
      """
      
* section[+]
  * title = "Section 11.5"
  * code = $NCIT#C218658  "ICH M11 Protocol Section 11.5 Insurance and Indemnity"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
The sponsor maintains adequate insurance coverage for participant injury arising from trial participation in accordance with applicable regulatory requirements. Compensation for trial-related injury will be provided in accordance with applicable national laws. Details of the indemnity arrangements are set out in the site agreement.
        </div>
      """
      
* section[+]
  * title = "Section 11.6"
  * code = $NCIT#C218659  "ICH M11 Protocol Section 11.6 Risk-Based Quality Management"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Critical-to-quality factors for this trial include accurate eligibility assessment, protocol-compliant dosing, complete primary endpoint data collection, and timely adverse event reporting. The sponsor will implement a risk-based quality management plan that identifies these risks, assigns probability and impact scores, and specifies mitigation actions and responsible parties. The plan will be reviewed and updated throughout the trial as new information emerges.
        </div>
      """
      
* section[+]
  * title = "Section 11.7"
  * code = $NCIT#C218660  "ICH M11 Protocol Section 11.7 Data Governance"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Electronic data capture (EDC) will be used for all trial data. The EDC system is validated and complies with 21 CFR Part 11 / Annex 11 requirements. Data entry will be performed by authorised, trained site staff. Audit trails will capture all data entries and modifications. Data will be transmitted securely and stored with restricted access. Data retention will comply with regulatory and sponsor requirements, with records retained for a minimum of 15 years after trial completion.
        </div>
      """
      
* section[+]
  * title = "Section 11.8"
  * code = $NCIT#C218661  "ICH M11 Protocol Section 11.8 Data Protection"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Personal data will be collected only to the extent necessary for the conduct of the trial and will be processed in accordance with the General Data Protection Regulation (GDPR), the UK Data Protection Act 2018, the Japanese APPI, and any other applicable national data protection legislation. Participant identifiers will be replaced by a unique trial number; personally identifiable information will not leave the site except where required by law. In the event of a data security breach, the sponsor and affected sites will notify the relevant supervisory authorities and affected individuals in accordance with applicable legal requirements.
        </div>
      """
      
* section[+]
  * title = "Section 11.9"
  * code = $NCIT#C218662  "ICH M11 Protocol Section 11.9 Source Records"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Source records are those documents and records from which trial data are derived. Accurate and complete source records are essential to verify the integrity of trial data, to demonstrate that the rights and welfare of participants have been protected, and to allow reconstruction of trial events. Participants will be identified only by their trial number and initials in sponsor documents; full identifiers are retained at site level as source data.
        </div>
      """     
  * section[+]
    * title = "Investigator Expectations for Source Records"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  Investigators must maintain complete and accurate source records for each participant and must make them available for review by the sponsor monitor, auditors, and regulatory inspectors upon request. Source records must be retained for the period specified in the site agreement and applicable regulations.
          </div>
        """
  * section[+]
    * title = "Trial Monitor Expectations for Source Records"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  Trial monitors will verify that source records exist for all data reported in the EDC, that the data are accurate, complete, and legible, and that participant rights and welfare have been protected. Monitoring will be performed according to a risk-based monitoring plan; the frequency and intensity of on-site visits will reflect the complexity and risk profile of the site.
          </div>
        """
  * section[+]
    * title = "Identification of Source Records"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  Source records include, but are not limited to: hospital or clinic medical records, laboratory reports, pharmacy records, radiological images, participant diaries, ECG tracings, and any other records used to record and confirm trial-specific observations and procedures. The investigator will maintain a source document identification log that specifies the source of each data item collected.
          </div>
        """
 

* section[+]
  * title = "Section 11.10"
  * code = $NCIT#C218663  "ICH M11 Protocol Section 11.10 Protocol Deviations"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Protocol deviations will be identified, documented, and assessed by the investigator and sponsor throughout the trial. Major deviations that may affect participant safety or data integrity will be reported to the IEC/IRB and regulatory authorities as required by applicable regulations. All deviations will be recorded in the site's deviation log. The sponsor's clinical operations team will review deviation trends as part of the ongoing risk-based quality management programme.
        </div>
      """
      
* section[+]
  * title = "Section 11.11"
  * code = $NCIT#C218664  "ICH M11 Protocol Section 11.11 Early Site Closure"
  * section[+]
    * title = "Decision Rights"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  The sponsor reserves the right to close a site at any time without prior notice if there is evidence of serious non-compliance, unacceptable risk to participants, or failure to recruit according to agreed timelines. The investigator may initiate early closure if, in their clinical judgement, continuation of the trial at the site would pose unacceptable risk to participants.
          </div>
        """
  * section[+]
    * title = "Investigator Criteria"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  Criteria for sponsor-initiated early closure include: persistent failure to comply with the protocol or GCP, unacceptably high deviation rates, failure to maintain adequate source records, loss of regulatory approval, or insufficient recruitment. Criteria for investigator-initiated early closure include: new information indicating unacceptable risk to participants, loss of IEC/IRB approval, or inability of the site to maintain adequate trial conduct.
          </div>
        """
  * section[+]
    * title = "Responsibilities"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  Upon early site closure, the investigator must notify the IEC/IRB and relevant regulatory authorities as required by local law. All ongoing participants must be transitioned to appropriate alternative therapy or follow-up under the investigator's care. All trial materials, including investigational medicinal product, must be accounted for and returned or disposed of according to sponsor instructions. Source records must be retained and remain accessible for the required retention period.
          </div>
        """
 

* section[+]
  * title = "Section 11.12"
  * code =  $NCIT#C218665  "ICH M11 Protocol Section 11.12 Data Dissemination"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
This trial will be registered in a publicly accessible clinical trials registry (e.g., ClinicalTrials.gov, EudraCT) prior to enrolment of the first participant, in accordance with ICH E9 and applicable regulatory requirements. Summary results will be posted to the registry within 12 months of the primary completion date. Publication of trial results will follow the sponsor's publication policy, which is consistent with good publication practice guidelines.
        </div>
      """
 