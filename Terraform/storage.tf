resource "aws_instance" "dbs" {
  instance_type     = "t3.small"
  availability_zone = var.target_region
  ami               = "ami-0194def630642c334"
  vpc_security_groups = [aws_security_group.global_group_for_proxy.id]
  user_data_replace_on_change = true

  user_data = <<-EOF
              #!bin/bash
              docker volume create sql-item-manage
              docker run \
                -e "ACCEPT_EULA=${var.accept_eula}" \
                -e "MSSQL_SA_PASSWORD=${var.database_password}" \
                -p 1433:1433 \
                -v sql-item-manage:/var/opt/mssql \
                -d \
                --name DndItem \
                mcr.microsoft.com/mssql/server
              EOF
}
