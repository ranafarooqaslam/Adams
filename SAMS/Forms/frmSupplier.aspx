<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSupplier.aspx.cs" Inherits="Forms_frmSupplier" Title="SAMS :: Add Supplier" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">

        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtSupplier.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Supplier Name');
                return false;
            }
            return true;
        }
    </script>
    <div id="right_data">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="87px" Text="Supplier Name" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" colspan="3">
                                                <asp:TextBox ID="txtSupplier" runat="server" Width="500px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblAddress" runat="server" Width="80px" Text="Address"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" colspan="3">
                                                <asp:TextBox ID="txtAddress" TextMode="MultiLine" runat="server" Width="500px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="90px" Text="Contact Person" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:TextBox ID="txtContactPerson" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                             <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblContactNo" runat="server" Width="69px" Text="Contact No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtContactNo" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="96px" Text="Email" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:TextBox ID="txtEmail" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px;padding-right: 0px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="96px" Text="NTN" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:TextBox ID="txtNTN" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>

                                            <td style="height: 25px;padding-left: 4px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="96px" Text="Sales Tax No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:TextBox ID="txtSalesTaxNo" runat="server" Width="188px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px">
                                            </td> 
                                            <td style="width: 200px; height: 26px">
                                                <asp:CheckBox ID="chkIsActive" runat="server" Width="97px" Text="Is Active" AutoPostBack="True"
                                                    Checked="True"></asp:CheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px; height: 32px" align="left">
                                            </td>
                                            <td style="width: 200px; height: 32px">
                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="84px" Font-Size="8pt"
                                                    Text="Save" ValidationGroup="vg" CssClass="Button" />
                                                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Width="83px"
                                                    Font-Size="8pt" Text="Cancel" CssClass="Button" />
                                            </td>
                                            <td style="width: 1px; height: 32px">
                                                <asp:HiddenField ID="txtRecordID" Value="0" runat="server" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <table class="tblhead" width="900px" style="display:none;">
                                        <tbody>
                                            <tr>
                                                <td style="color: White; font-weight: bold;">
                                                    <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                                </td>
                                                <td style="width: 170px;">
                                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                                        <asp:ListItem Value="SKU_code">All Records</asp:ListItem>
                                                        <asp:ListItem Value="Distributor_Code">Distributor Code</asp:ListItem>
                                                        <asp:ListItem Value="Distributor_Name">Distributor Name</asp:ListItem>
                                                        <asp:ListItem Value="Contact_Person">Fax #</asp:ListItem>
                                                        <asp:ListItem Value="CONTACT_NUMBER">Contact No</asp:ListItem>
                                                        <asp:ListItem Value="TYPENAME">Distributor Type</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 224px;">
                                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                                </td>
                                                <td style="width:250px;">
                                                    <asp:Button ID="btnFilter" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                                        OnClick="btnFilter_Click" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                <asp:UpdatePanel ID="id" runat="server" >
                                 <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server" BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px" ScrollBars="Vertical" Height="200" Width="900px">
                                        <asp:GridView ID="GridAssetType" runat="server" Width="99%" ForeColor="SteelBlue"
                                            CssClass="gridRow2" AutoGenerateColumns="False" BackColor="White" BorderColor="White"
                                            OnPageIndexChanging="GridAssetType_PageIndexChanging" OnRowCommand="GridAssetType_RowCommand"
                                            HorizontalAlign="Center">
                                            <Columns>
                                                <asp:BoundField DataField="Supplier_ID" HeaderText="Supplier_ID">
                                                     <HeaderStyle CssClass="HidePanel" />
                                                     <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier">
                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Address" HeaderText="Address">
                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person">
                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ContactNo" HeaderText="Contact No">
                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Email" HeaderText="Email">
                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NTN" HeaderText="NTN">
                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SalesTaxNo" HeaderText="Sales Tax No">
                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IsActive" HeaderText="Status">
                                                    <ItemStyle Width="65px" HorizontalAlign="Center" BorderStyle="Solid" BorderColor="Silver"
                                                        BorderWidth="1px" VerticalAlign="Top"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="edt"  Text="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                        Width="35px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                        </asp:GridView>
                                    </asp:Panel>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
