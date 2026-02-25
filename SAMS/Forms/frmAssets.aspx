<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmAssets.aspx.cs" Inherits="Forms_frmAssets" Title="SAMS: Assets Opening" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("select").searchable();
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnDone.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnDone.ClientID%>').disabled = false;

        }

        function TotalDebitCredit(objDebit, objCredit) {
            var gv = document.getElementById('<%= gvOpening.ClientID %>');
            var tb = gv.getElementsByTagName("input");


            if (parseFloat(objDebit.value.replace(',', '')) > 0) {
            }
            else {
                objDebit.value = 0;
            }

            if (parseFloat(objCredit.value.replace(',', '')) > 0) {
            }
            else {
                objCredit.value = 0;
            }

            var TotalDebit = 0;
            for (var i = 0; i < tb.length; i++) {
                if (tb[i].type == "text") {
                    if (tb[i].name.indexOf("txtDebit") !== -1) {
                        var objValue = parseFloat(tb[i].value.replace(',', ''));
                        if (parseFloat(objValue) > 0) {
                            TotalDebit += parseFloat(objValue);
                        }
                    }
                }
            }

            document.getElementById("<%= txtDebit.ClientID %>").value = numberWithCommas((parseFloat(TotalDebit)).toFixed(2));

            var TotalCredit = 0;
            for (var i = 0; i < tb.length; i++) {
                if (tb[i].type == "text") {
                    if (tb[i].name.indexOf("txtCredit") !== -1) {
                        var objValue = parseFloat(tb[i].value.replace(',', ''));
                        if (parseFloat(objValue) > 0) {
                            TotalCredit += parseFloat(objValue);
                        }
                    }
                }
            }

            document.getElementById("<%= txtCredit.ClientID %>").value = numberWithCommas((parseFloat(TotalCredit)).toFixed(2));
        }

        function numberWithCommas(x) {
            x = x.toString();
            var pattern = /(-?\d+)(\d{3})/;
            while (pattern.test(x))
                x = x.replace(pattern, "$1,$2");
            return x;
        }

    </script>
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                 <td style="width: 87px"></td>                                        
                                        <td style="width: 9%" align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Text="Location:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 84%">
                                            <asp:DropDownList ID="drpDistributor" runat="server" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="true" Width="200px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td style="width: 87px"></td>
                                        <td style="width: 9%" align="left">
                                            <strong>
                                                <asp:Label ID="lblDueDate" runat="server" Text="Voucher Date:" 
                                                Width="100px" Height="16px"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 84%">
                                            <asp:TextBox ID="txtVoucherDate" runat="server" Width="177px" onkeyup="BlockEndDateKeyPress()"></asp:TextBox>
                                            <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                            <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                                TargetControlID="txtVoucherDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td style="width: 87px"></td>
                                        <td style="width: 9%" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Text="Narration:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 84%">
                                            <asp:TextBox ID="txtNarration" runat="server" Width="555px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td style="width: 87px"></td>
                                        <td colspan="3">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                   
                                        <td colspan="4" style="width: 100%">
                                            <asp:Panel ID="Panel2" runat="server" Width="790px" Height="280px" ScrollBars="Vertical"
                                                BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                <asp:GridView ID="gvOpening" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                    BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center" Width="100%"
                                                    OnRowDataBound="gvOpening_RowDataBound">
                                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                        PreviousPageText="Previous" />
                                                    <RowStyle ForeColor="Black" />
                                                    <Columns>
                                                        <asp:BoundField DataField="ACCOUNT_HEAD_ID" HeaderText="ACCOUNT_HEAD_ID">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ACCOUNT_NAME" HeaderText="Account Description">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                Width="180px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRemarks" runat="server" Width="100%"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                Width="150px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Debit">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDebit" runat="server" Width="100%"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDebit"
                                                                    FilterType="Custom" ValidChars=".0123456789">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                Width="80px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Credit">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCredit" runat="server" Width="100%"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtCredit"
                                                                    FilterType="Custom" ValidChars=".0123456789">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                Width="80px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="White" />
                                                    <PagerStyle BackColor="Transparent" />
                                                    <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                    
                                        <td colspan="4">
                                            <table style="text-align: right; width: 80%;">
                                                <tr>
                                                    <td style="width: 66%;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 17.5%;">
                                                        <asp:TextBox ID="txtDebit" runat="server" Width="120px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 17.5%;">
                                                        <asp:TextBox ID="txtCredit" runat="server" Width="114px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                   
                                        <td colspan="4">
                                            <div style="z-index: 101; left: 806px; width: 100px; position: absolute; top: 364px;
                                                height: 100px">
                                                <asp:Panel ID="Panel21" runat="server">
                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                        <ProgressTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" Height="35px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                                Width="39px" />
                                                            Wait Update
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                    
                                    
                                        <td colspan="4">
                                            <asp:Button ID="btnDone" runat="server" AccessKey="S" OnClick="btnDone_Click" Text="Save"
                                                CssClass="Button" />
                                            &nbsp;<asp:Button ID="btnCancel" runat="server" AccessKey="S" OnClick="btnCancel_Click"
                                                Text="Cancel" CssClass="Button" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
