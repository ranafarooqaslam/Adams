<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDistributorMapping.aspx.cs" Inherits="Forms_frmDistributorMapping"
    Title="SAMS :: Distributor Mapping" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
<script language="JavaScript" type="text/javascript"> 
function pageLoad() {
            $("select").searchable();
  }
    </script>

    <div id="right_data">
        <div style="z-index: 101; left: 481px; width: 100px; position: absolute; top: 282px; height: 100px"
            id="DIV1">
            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" Width="28px" Height="22px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:ImageButton>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table cellpadding="3" cellspacing="3">
                    <tbody>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblmsg" runat="server"  ForeColor="Red"></asp:Label>
                            </td>
                            <td></td>

                        </tr>
                        <tr>
                            <td style="width: 70px">
                                <strong>Location</strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlLocation" runat="server" Width="200px"
                                    OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 70px">
                                <strong>Distributor</strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddDistributorType" runat="server" Width="200px" CssClass="DropList">
                                    <%--OnSelectedIndexChanged="ddDistributorType_SelectedIndexChanged" AutoPostBack="True">--%>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><strong>Customer </strong>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddCustomer" runat="server" Width="200px" CssClass="DropList">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">

                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="90px" CssClass="Button"
                                    Text="Save"></asp:Button>

                            </td>
                        </tr>

                    </tbody>
                </table>
                <table width="50%">
                    <tr>
                        <td>
                            <asp:GridView ID="GrdDistributor" runat="server" Width="100%" ForeColor="SteelBlue"
                                OnRowDeleting="GrdDistributor_RowDeleting"
                                BorderColor="White" HorizontalAlign="Center"
                                BackColor="White" AutoGenerateColumns="False"
                                OnRowEditing="GrdDistributor_RowEditing">

                                <Columns>
                                    <asp:BoundField DataField="DISTRIBUTOR_ID">
                                        <HeaderStyle CssClass="HidePanel" />
                                        <ItemStyle CssClass="HidePanel" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Distributor">
                                        <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer Name">
                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CUSTOMER_ID">

                                        <HeaderStyle CssClass="HidePanel" />
                                        <ItemStyle CssClass="HidePanel" />
                                    </asp:BoundField>
                                    <asp:CommandField ShowEditButton="True" Visible="false">
                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"  HorizontalAlign="Center"/>
                                    </asp:CommandField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                Text="Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="tblhead" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
