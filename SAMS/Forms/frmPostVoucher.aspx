<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPostVoucher.aspx.cs" Inherits="Forms_frmPostVoucher" Title="SAMS :: Voucher Posting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript">
    </script>
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        
        .modalPopup
        {
            background-color: #ffffdd;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 350px;
        }
    </style>
    <div style="z-index: 101; left: 497px; width: 100px; position: absolute; top: 409px;
        height: 100px">
        <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup" Style="display: none;
            height: 50px; width: 85px; padding: 10px">
        </asp:Panel>
        <cc1:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
            TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="programmaticPopup"
            BackgroundCssClass="modalBackground" DropShadow="True" RepositionMode="RepositionOnWindowScroll">
        </cc1:ModalPopupExtender>
        <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
    </div>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" Width="67px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="104px" Text="Voucher Type" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="DrpVoucherType" runat="server" Width="200px" CssClass="DropList">
                                                    <asp:ListItem Value="14">Cash Voucher</asp:ListItem>
                                                    <asp:ListItem Value="15">Bank Voucher</asp:ListItem>
                                                    <asp:ListItem Value="16">Journal Voucher</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" valign="middle" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="104px" Text="Principal" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 100px" align="left">
                                                <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" valign="middle" align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="70px" Height="13px" Text="From Date"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtStartDate" onkeyup="BlockStartDateKeyPress()" runat="server"
                                                    Width="150px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="ibtnStartDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                                </asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" valign="middle" align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="80px" Height="13px" Text="To Date"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEndDate" onkeyup="BlockEndDateKeyPress()" runat="server" Width="150px"
                                                    CssClass="txtBox " MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="ibnEndDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                                </asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <cc1:CalendarExtender ID="CEStartDate" runat="server" TargetControlID="txtStartDate"
                                                    PopupButtonID="ibtnStartDate" Format="dd-MMM-yyyy">
                                                </cc1:CalendarExtender>
                                                <cc1:CalendarExtender ID="CEEndDate" runat="server" TargetControlID="txtEndDate"
                                                    PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 12px" valign="top" align="left">
                                                <asp:CheckBox ID="ChbSelect" runat="server" Width="115px" Text="All Select" OnCheckedChanged="ChbSelect_CheckedChanged"
                                                    AutoPostBack="True"></asp:CheckBox>
                                            </td>
                                            <td style="width: 100px; height: 12px" align="left">
                                                <asp:Button ID="btnView" OnClick="btnView_Click" runat="server" Width="80px" Font-Size="8pt"
                                                    Text="View" CssClass="Button" OnClientClick="showPopup()" />
                                                <asp:Button ID="btnPost" runat="server" Width="80px" Font-Size="8pt" Text="Post"
                                                    OnClick="btnPost_Click" CssClass="Button" OnClientClick="showPopup()" />
                                                    
                                            </td>
                                            <td style="height: 25px" align="left">
                                             
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>                                 
                                <asp:Panel ID="Panel2" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    Height="150px" ScrollBars="Vertical" Width="850px">
                                    <asp:GridView ID="GrdLedger" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                        OnRowCommand="GrdLedger_RowCommand" Width="100%">
                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                            PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Voucher">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChbSelect" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Ledger_date" HeaderText="Voucher Date">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                              <asp:BoundField DataField="Debit" HeaderText="Amount">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="VOUCHER_TYPE_ID" HeaderText="VOUCHER_TYPE_ID">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="VOUCHER_DATE" HeaderText="VOUCHER_DATE">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="edt"  Text="Edit"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="35px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>
                                </asp:Panel>
                    </td>
                </tr>
                <tr>
                <td>
                
               
                </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                                <asp:Panel ID="Panel1" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    Height="150px" ScrollBars="Vertical" Width="850px">
                                    <asp:GridView ID="GrdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="White" CaptionAlign="Left" CssClass="gridRow2" ForeColor="SteelBlue"
                                        HorizontalAlign="Center" Width="100%" OnRowDataBound="GrdOrder_RowDataBound" OnRowCommand="GrdOrder_RowCommand" 
                                        ShowFooter="True">
                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                            PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Ledger_date" HeaderText="Voucher Date">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                              <asp:BoundField DataField="Debit" HeaderText="Amount">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                             <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="edt"  Text="Detail"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="35px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>
                                </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
