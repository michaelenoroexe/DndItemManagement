resource "aws_security_group" "global_group_for_proxy" {
  name = "global-access-group"

  ingress {
    from_port   = -1
    to_port     = -1
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }
}
