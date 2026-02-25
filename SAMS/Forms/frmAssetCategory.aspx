<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmAssetCategory.aspx.cs" Inherits="Forms_frmAssetCategory" Title="SAMS :: Asset Category" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">

        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtAssetType.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Asset Type');
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
                                                    <asp:Label ID="Label3" runat="server" Width="83px" Text="Asset Type" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtAssetType" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="height: 25px">
                                                &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblDescription" runat="server" Width="80px" Text="Description"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" colspan="4">
                                                <asp:TextBox TextMode="MultiLine" ID="txtDescription" runat="server" Width="400px"></asp:TextBox>
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
                                                <asp:BoundField DataField="AssetCategoryID" HeaderText="AssetCategoryID">
                                                     <HeaderStyle CssClass="HidePanel" />
                                                     <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CategoryName" HeaderText="Asset Category">
                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Description" HeaderText="Description">
                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IS_DELETED" HeaderText="Status">
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
