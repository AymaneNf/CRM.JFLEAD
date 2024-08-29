export enum LeadType {
  Societe = 0,
  Individu = 1
}

export enum LeadStatus {
  Nouveau = 0,
  Assigne = 1,
  EnCours = 2,
  ConvertiGagne = 3,
  ConvertiPerdu = 4
}

export interface Lead {
  id: string;                // Corresponding to BaseEntity's Id property
  type: LeadType;            // Enum or string, depending on how you define LeadType in your Angular app
  denomination?: string;     // For Societe
  civilite?: string;         // For Individu
  nom?: string;              // Last name
  prenom?: string;           // First name
  adresse?: string;          // Address
  codePostale?: string;      // Postal code
  ville?: string;            // City
  region?: string;           // Region
  pays?: string;             // Country
  telephone?: string;        // Phone number
  fax?: string;              // Fax number
  email?: string;            // Email address
  siteWeb?: string;          // Website
  categorie?: string;        // Category
  secteurActivite?: string;  // Industry sector
  provenance?: string;       // Origin
  status: LeadStatus;        // Enum or string, depending on how you define LeadStatus in your Angular app
  assignedTo: number;        // Assigned collaborator's ID
}
