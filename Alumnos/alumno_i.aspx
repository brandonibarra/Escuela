<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="alumno_i.aspx.cs" Inherits="Escuela.Alumnos.alumno_i" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
			<table>

				<tr>
					<td>Matricula :</td>
					<td>
					<asp:TextBox ID="txtMatricula" MaxLength="8" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfv_matricula" runat="server" ControlToValidate ="txtMatricula"
					ErrorMessage="La matricula es requerida" ValidationGroup="vlg1" Display="Dynamic"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="rev_matricula" ControlToValidate ="txtMatricula" ValidationExpression="^[0-9]+$"
					runat="server" ErrorMessage="Solo se aceptan numeros enteros" ValidationGroup="vlg1" Display="Dynamic"></asp:RegularExpressionValidator>
					</td>
				</tr>

				<tr>
					<td>Nombre :</td>
					<td> <asp:TextBox ID="txtNombre" MaxLength="100" runat="server"></asp:TextBox> 
					<asp:RequiredFieldValidator ID="rfv_nombre" runat="server" ControlToValidate ="txtNombre"
					ErrorMessage="El nombre es requerido" ValidationGroup="vlg1" Display="Dynamic"></asp:RequiredFieldValidator>
					</td>
				</tr>

				<tr>
					<td>Fecha de Nacimiento :</td>
					<td> <asp:TextBox ID="txtFechaNacimento" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate ="txtFechaNacimento"
					ErrorMessage="La fecha de nacimiento es requerida" ValidationGroup="vlg1" Display="Dynamic"></asp:RequiredFieldValidator>
					<asp:CompareValidator ID="cv_fecha" runat="server" ControlToValidate="txtFechaNacimento"
					Type="Date" Operator="DataTypeCheck" ValidationGroup="vlg1"
					ErrorMessage="El formato es incorrecto (dd/mm/yyyy) o (mm/dd/yyyy)" Display="Dynamic"></asp:CompareValidator>
					</td>
				</tr>

				<tr>
					<td>Semestre :</td>
					<td> <asp:TextBox ID="txtSemestre" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfv_semestre" runat="server" ControlToValidate ="txtSemestre"
					ErrorMessage="El semestre es requerido" ValidationGroup="vlg1" Display="Dynamic"></asp:RequiredFieldValidator>
					<asp:RangeValidator ID="rv_semestre" runat="server" ErrorMessage="El semestre debe de ser entero de 1 a 12"
					Type="Integer" MinimumValue="1" MaximumValue="12" ControlToValidate ="txtSemestre" ValidationGroup="vlg1" Display="Dynamic"></asp:RangeValidator>
					</td>
				</tr>

				<tr>
					<td>Facultad :</td>
					<td> <asp:DropDownList ID="ddlFacultad" CssClass="lista" runat="server"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfv_facultad" runat="server" ControlToValidate ="ddlFacultad"
					ErrorMessage="La facultad es requerida" InitialValue="0" ValidationGroup="vlg1" Display="Dynamic"></asp:RequiredFieldValidator>
					</td>
				</tr>

				<tr>
					<td>Estado :</td>
					<td> <asp:DropDownList ID="ddlEstado" CssClass="lista" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged"></asp:DropDownList>
				
					</td>
				</tr>

				<tr>
					<td>Ciudad :</td>
					<td> <asp:DropDownList ID="ddlCiudad" CssClass="lista" runat="server"></asp:DropDownList>
				
					</td>
				</tr>

				<tr>
					<td>Materias:</td>
					<td> <asp:ListBox ID="listBoxMaterias" SelectionMode="Multiple" CssClass="lista" Width="150px" runat="server"></asp:ListBox>
				
					</td>
				</tr>

				<tr>
					<td></td>
					<td> <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click"  ValidationGroup="vlg1" />  </td>
				</tr>

			</table>

			</ContentTemplate>
    </asp:UpdatePanel>

    <asp:GridView ID="grd_alumnos" AutoGenerateColumns="false" runat="server">
		<Columns>
			<asp:BoundField  HeaderText="Matricula" DataField="matricula"/>
			<asp:BoundField  HeaderText="Nombre" DataField="nombre"/>
		</Columns>
    </asp:GridView>

	<script type="text/javascript">

		$(document).ready(function () {
			$("#MainContent_txtFechaNacimento").datepicker({
				changeMonth: true,
				changeYear: true,
				yearRange: "1960:2010",
				dateFormat:"dd-mm-yy"
				
			});

			$(".lista").chosen();

		});

		var manager = Sys.WebForms.PageRequestManager.getInstance();

		manager.add_endRequest(function () {
            $(document).ready(function () {
                $("#MainContent_txtFechaNacimento").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "1960:2010",
                    dateFormat: "dd-mm-yy"

                });

                $(".lista").chosen();

            });


        })

    </script>

</asp:Content>
