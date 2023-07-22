variable "target_region" {
  description = "Target region of infrastructure"
  type        = string
  default     = "eu-north-1"
}

variable "accept_eula" {
  description = "Accepting eula of MSSQL"
  type        = string
  default     = "Y"
}

variable "database_password" {
  description = "Password of main database of application"
  type        = string
  sensitive   = true
}