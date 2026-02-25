<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmAccountHeadAssignment2.aspx.cs" Inherits="Forms_frmAccountHeadAssignment2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script type="text/javascript" language="javascript">
        function ddlFocus(obj) {
            obj.className = "ddlFocus";
        }

        function ddlBlur(obj) {
            obj.className = "";
        }
        function pageLoad() {

            $("select").searchable();
        }
    </script>
    <div id="right_data">

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table>
                    <tr height="30px">
                        <td width="110px">
                            <strong>Channel Type</strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="DrpChannelType" runat="server" Width="205px" CssClass="DropList">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr height="30px">
                        <td width="110px">
                            <strong>Credit Acc/Head</strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="DrpCreditHead" runat="server" Width="260px" onfocus="ddlFocus(this);"
                                onblur="ddlBlur(this);">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr height="30px">
                        <td width="110px">
                            <strong>Cash Acc/Head</strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="DrpCashHead" runat="server" Width="260px" onfocus="ddlFocus(this);"
                                onblur="ddlBlur(this);">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" Width="80px"
                                OnClick="btnSave_Click" />&nbsp;&nbsp;
                            
                            <asp:HiddenField ID="hf_ID" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="80%">
                    <tr>
                        <td>
                        <asp:Panel ID="pnl_grid" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="250px" ScrollBars="Vertical">
                            <asp:GridView ID="GrdAccount" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2"
                                OnRowDeleting="GrdAccount_RowDeleting" BorderColor="White" HorizontalAlign="Center"
                                BackColor="White" AutoGenerateColumns="False" OnRowCommand="GrdAccount_RowCommand">
                                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                    PreviousPageText="Previous" />
                                <Columns>
                                    <asp:BoundField DataField="ACCOUNT_ASSIGN_ID" HeaderText="ACCOUNT_ASSIGN_ID">
                                        <HeaderStyle CssClass="HidePanel" />
                                        <ItemStyle CssClass="HidePanel" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CHANNEL_TYPE_ID" HeaderText="CHANNEL_TYPE_ID">
                                        <FooterStyle CssClass="HidePanel" />
                                        <HeaderStyle CssClass="HidePanel" />
                                        <ItemStyle CssClass="HidePanel" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CREDIT_HEAD_ID" HeaderText="CREDIT_HEAD_ID">
                                        <FooterStyle CssClass="HidePanel" />
                                        <HeaderStyle CssClass="HidePanel" />
                                        <ItemStyle CssClass="HidePanel" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CASH_HEAD_ID" HeaderText="CASH_HEAD_ID">
                                        <FooterStyle CssClass="HidePanel" />
                                        <HeaderStyle CssClass="HidePanel" />
                                        <ItemStyle CssClass="HidePanel" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CHANNEL_TYPE_NAME" HeaderText="Channel Type">
                                        <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CREDIT_ACCOUNT_NAME" HeaderText="Credit Account Head">
                                        <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CASH_ACCOUNT_NAME" HeaderText="Cash Account Head">
                                        <HeaderStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="edt"  Text="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                            Width="35px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">                                 
                                        <ItemTemplate >
                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                Text="Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="50px"/>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="tblhead" />
                            </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                  <div style="z-index: 101; left: 600px; width: 100px; position: absolute; top: 100px;
                            height: 100px">
                            &nbsp;<asp:Panel ID="Panel21" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="31px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
