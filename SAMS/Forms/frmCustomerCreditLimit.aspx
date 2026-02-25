<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmCustomerCreditLimit.aspx.cs" Inherits="Forms_frmCustomerCreditLimit"
    Title="SAMS :: Customer Assignement" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $('#<%=Grid_users.ClientID %>').tablesorter(
	     {
	         headers: {
	             8: {
	                 sorter: false
	             }
	         }
	     });
        }
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 100px">
                        <table>
                            <tr>
                                <td style="width: 100px">
                                    <strong>
                                        <asp:Label ID="Label7" runat="server" CssClass="lblbox" Text="Location" Width="77px"></asp:Label></strong>
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DrpDistributor" runat="server" CssClass="DropList" Width="205px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table class="tblhead" width="901px">
                                    <tbody>
                                        <tr>
                                            <td style="color: White; font-weight: bold; ">
                                                <asp:Label ID="Label10" runat="server" Width="300px" Text="Select Searching Type"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                                    <asp:ListItem Value="CUSTOMER_CODE">Customer Code</asp:ListItem>
                                                    <asp:ListItem Value="CUSTOMER_NAME">Customer Name</asp:ListItem>
                                                    <asp:ListItem Value="CONTACT_PERSON">Contact Person</asp:ListItem>
                                                    <asp:ListItem Value="CONTACT_NUMBER">Contact Number</asp:ListItem>
                                                    <asp:ListItem Value="ADDRESS">Address</asp:ListItem>
                                                    <asp:ListItem Value="EMAIL_ADDRESS">Email Address</asp:ListItem>
                                                    <asp:ListItem Value="GEO_NAME">Town Name</asp:ListItem>
                                                    <asp:ListItem Value="AREA_NAME">Route Name</asp:ListItem>
                                                    <asp:ListItem Value="ROUTE_NAME">Market Name</asp:ListItem>
                                                    <asp:ListItem Value="SLASH_DESC">Channel Type</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 224px; height: 21px" align="left">
                                                <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="height: 21px; width: 145px" align="left">
                                                <asp:Button ID="btnSearch" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                                    OnClick="btnSearch_Click"></asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Panel ID="Panel2" runat="server" Width="900px" Height="150px" ScrollBars="Vertical"
                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                    <asp:GridView ID="Grid_users" runat="server" Width="99%" ForeColor="SteelBlue" CssClass="tablesorter"
                                        BorderColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White"
                                        OnRowCommand="Grid_users_RowCommand">
                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                            PreviousPageText="Previous"></PagerSettings>
                                        <Columns>
                                            <asp:BoundField DataField="CUSTOMER_ID" HeaderText="Customer Id">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DISTRIBUTOR_ID" HeaderText="DISTRIBUTOR_ID">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                                </ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Code">
                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name">
                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CONTACT_NUMBER" HeaderText="Contact Number">
                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GEO_NAME" HeaderText="Town">
                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AREA_NAME" HeaderText="Route">
                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ROUTE_NAME" HeaderText="Market">
                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Select">
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
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 100px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="Panel3" runat="server" BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px"
                                    Height="208px" ScrollBars="Vertical" Width="900px">
                                    <asp:GridView ID="GrdCreditLimit" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="White" CssClass="gridRow2" ForeColor="Silver" HorizontalAlign="Center"
                                        Width="99.5%">
                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                            PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField DataField="Company_Id" HeaderText="Company_Id">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Assign" Visible="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChbSelect" runat="server" Width="40px" />
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Company_Name" HeaderText="Principal"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Credit Limit">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCreditLimit" runat="server" CssClass="txtBox" Width="99%"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="99px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow Days">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCreditDays" runat="server" CssClass="txtBox " Width="99%"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Channel" Visible=false>
                                             <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpChannelType" runat="server" CssClass="DropList" Width="99%">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category" Visible=false>
                                             <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DrpBusinessType" runat="server" CssClass="DropList" Width="99%">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Class" Visible=false>
                                             <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DrpVolumeClass" runat="server" CssClass="DropList" Width="99%">
                                                        <asp:ListItem>A</asp:ListItem>
                                                        <asp:ListItem>B</asp:ListItem>
                                                        <asp:ListItem>C</asp:ListItem>
                                                        <asp:ListItem>D</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible=false>
                                             <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DrpType" runat="server" Width="99%">
                                                        <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                                        <asp:ListItem Value="Bill">Bill</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="tblhead" />
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div style="z-index: 101; left: 540px; width: 100px; position: absolute; top: 150px;
                            height: 100px">
                            &nbsp;<asp:Panel ID="Panel1" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="23px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                        <br />
                        <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Save"
                            ValidationGroup="vg" Width="89px" CssClass="Button" />
                        <asp:Button ID="btnCancel" runat="server" Font-Size="8pt" Text="Cancel" Width="91px"
                            CssClass="Button" />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
