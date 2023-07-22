variable "encrypt" {
  description = "Encryption type of images"
  type        = string
  default     = "KMS"
}
variable "tags" {
  description = "Aws resources tags"
  type        = map(string)
  default = {
    Environment = "Dnd-item-manager"
    Service     = "Prod"
  }
}

resource "aws_ecr_repository" "api" {
  name = "dnd-item-manager-api"
  encryption_configuration {
    encryption_type = var.encrypt
  }
  image_scanning_configuration {
    scan_on_push = true
  }
  tags = var.tags
}
resource "aws_ecr_repository" "admin" {
  name = "dnd-item-manager-admin"
  encryption_configuration {
    encryption_type = var.encrypt
  }
  image_scanning_configuration {
    scan_on_push = true
  }
  tags = var.tags
}
resource "aws_ecr_repository" "client" {
  name = "dnd-item-manager-client"
  encryption_configuration {
    encryption_type = var.encrypt
  }
  image_scanning_configuration {
    scan_on_push = true
  }
  tags = var.tags
}
