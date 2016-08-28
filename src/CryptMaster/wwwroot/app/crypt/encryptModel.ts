type CryptTypeOptions = "encryption" | "decryption";

export class EncryptModel {
  constructor(
    public text: string,
    public type: string,
    public outputText: string,
    public cryptType: CryptTypeOptions
  ) {  }
}
