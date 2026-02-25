<%@ Page Title="SAMS :: Cheque Entry" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmChequeEntryView.aspx.cs" Inherits="Forms_frmChequeEntryView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
<%-- ReSharper disable WrongExpressionStatement --%>
    <script type="text/javascript">
        function pageLoad() {

            document.getElementById("<%= ibtnStartDate.ClientID %>").disabled = 'disabled';
            document.getElementById("<%= ibnEndDate.ClientID %>").disabled = 'disabled';

            ///////////Search for Cheques////////////////////
            $("#txtCheque").keyup(function (e) {
                $("#tblCheque tr:has(td)").hide();
                var iCounter = 0;
                var sSearchTerm = $("#txtCheque").val(); //Get the search box value
                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#tblCheque tr:has(td)").show();
                    return false;
                }
                $("#tblCheque tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        
                        return true;
                    }
                    
                });
               
            });
   



            ///////////Search for Customer////////////////////
            $("#txtCustomer").keyup(function (e) {
                $("#tblCustomer tr:has(td)").hide();
                var iCounter = 0;
                var sSearchTerm = $("#txtCustomer").val(); //Get the search box value
                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#tblCustomer tr:has(td)").show();
                    return false;
                }
                $("#tblCustomer tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        

                        return true;
                    }
                });
            });


        }

       
        function EnableDisableImageButtons() {
            var chk = document.getElementById("<%= cbDate.ClientID %>");
            if (chk.checked) {
                document.getElementById("<%= ibtnStartDate.ClientID %>").disabled = '';
                document.getElementById("<%= ibnEndDate.ClientID %>").disabled = '';
            }
            else {
                document.getElementById("<%= ibtnStartDate.ClientID %>").disabled = 'disabled';
                document.getElementById("<%= ibnEndDate.ClientID %>").disabled = 'disabled';

                document.getElementById("<%= txtDateFrom.ClientID %>").value = '';
                document.getElementById("<%= txtDateTo.ClientID %>").value = '';
            }
        }

        function SelectAllCheckboxesSpecific(spanChk) {

            var IsChecked = spanChk.checked;
            var Chk = spanChk;
            Parent = document.getElementById('tblCustomer');

            for (var i = 0, row; row = Parent.rows[i]; i++) {

                if ($(row).css("display") == 'none') {
                    
                }
                else {

                    if (inputList = row.cells[0].getElementsByTagName("input")) {
                        for (var n = 0; n < inputList.length; n++) {
                            if (inputList[n].type == "checkbox") {

                                inputList[n].checked = IsChecked;
                            }
                        }

                    }

                }
            }
        }
        
            
   

    </script>
<%-- ReSharper restore WrongExpressionStatement --%>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="upCheque" runat="server">
                            <ContentTemplate>
                                <div style="z-index: 101; left: 480px; width: 100px; position: absolute; top: 80px;
                                    height: 100px">
                                    <asp:Panel ID="Panel21" runat="server">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upCheque">
                                            <ProgressTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                    Width="23px" />
                                                Wait Update
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </asp:Panel>
                                </div>
                                <asp:Panel ID="pnlDropdowns" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="3">
                                                <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                                    DropShadow="true" PopupControlID="pnlCustomer" TargetControlID="btnAddCheque"
                                                    CancelControlID="btnCancel">
                                                </cc1:ModalPopupExtender>
                                                <br />
                                                <asp:Panel ID="pnlCustomer" runat="server" Style="display: none; background-color: White;"
                                                    ScrollBars="Auto" Height="540px" Width="105%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 100%" colspan="3">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 5%">
                                                            </td>
                                                            <td style="width: 100%" colspan="2">
                                                                <asp:CheckBox ID="cbDate" runat="server" onclick="EnableDisableImageButtons();" />
                                                                <strong>Date From</strong>
                                                                <asp:TextBox ID="txtDateFrom" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                                    Width="16px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <strong>Date To</strong>
                                                                <asp:TextBox ID="txtDateTo" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                                    Width="16px" />
                                                                <%@register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                                                <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                                                    TargetControlID="txtDateFrom">
                                                                </cc1:CalendarExtender>
                                                                <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                                                    TargetControlID="txtDateTo">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td style="width: 5%">
                                                            </td> <td style="width: 100%" colspan="2">
                                                               <asp:CheckBox ID="ChbAllCatagories" runat="server" Text="Select All" onclick="SelectAllCheckboxesSpecific(this);" >
                                                                </asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%" colspan="3">
                                                                &nbsp;&nbsp;<input type="text" id="txtCustomer" name="n" style="width: 430px; margin-left: 22px;"
                                                                    placeholder="Enter Text To Search " />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 5%">
                                                            </td>
                                                            <td style="width: 95%" colspan="2">
                                                                <asp:Panel ID="pnlCustomer2" runat="server" Height="380px" ScrollBars="Vertical"
                                                                    BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="430px">
                                                                    <asp:Repeater ID="rptCustomers" runat="server">
                                                                        <HeaderTemplate>
                                                                            <table style="width: 90%; margin-top: 0px">
                                                                                <tbody id="tblCustomer">
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <tr id="RowID">
                                                                                <td >
                                                                                    <asp:HiddenField ID="hfCUSTOMER_ID" runat="server" Value='<%# Eval("CUSTOMER_ID")%>' />
                                                                                    <asp:CheckBox ID="cbCustomer" CssClass="cbCustomerClass" runat="server" name="CHB" />
                                                                                  
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("CUSTOMER_DETAIL")%>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <AlternatingItemTemplate>
                                                                            <tr  id="RowID">
                                                                                <td>
                                                                                    <asp:HiddenField ID="hfCUSTOMER_ID" runat="server" Value='<%# Eval("CUSTOMER_ID")%>' />
                                                                                    <asp:CheckBox ID="cbCustomer" CssClass="cbCustomerClass" runat="server" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("CUSTOMER_DETAIL")%>
                                                                                </td>
                                                                            </tr>
                                                                        </AlternatingItemTemplate>
                                                                        <FooterTemplate>
                                                                            </tbody>
                                                                            </table>
                                                                        </FooterTemplate>
                                                                    </asp:Repeater>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="width: 80%" align="center">
                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" Width="59px"/>
                                                                <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="button" OnClick="btnNext_Click"  Width="59px"/>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 5%">
                                            </td>
                                            <td style="width: 10%">
                                                <strong>
                                                    <asp:Label ID="lblType" runat="server" Text="Cheque Type"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="width: 85%">
                                                <asp:RadioButtonList ID="rblChequeType" runat="server" RepeatDirection="Horizontal" 
                                                    OnSelectedIndexChanged="rblChequeType_SelectedIndexChanged"
                                                    AutoPostBack="True" Width="226px">
                                                    <asp:ListItem Text="Default" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Cheque Advance" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 5%">
                                            </td>
                                            <td style="width: 10%">
                                                <strong>
                                                    <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="width: 85%">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="226px" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 5%">
                                            </td>
                                            <td style="width: 10%">
                                                <strong>
                                                    <asp:Label ID="lblPrincipal" runat="server" Text="Principal"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="width: 85%">
                                                <asp:DropDownList ID="DrpPrincipal" runat="server" Width="226px" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 5%">
                                            </td>
                                            <td style="width: 10%">
                                                <strong>
                                                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="width: 85%">
                                                <asp:DropDownList ID="DrpStatus" runat="server" Width="226px" OnSelectedIndexChanged="DrpStatus_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <input type="text" id="txtCheque" name="n" style="width: 500px; height: 18px; margin-left: 55px;"
                                    placeholder="Enter Text To Search "/>
                                <asp:LinkButton ID="btnAddCheque" runat="server" 
                                    Style="float: right; height: 7px;">Add New Cheque</asp:LinkButton>
                                <br />
                                <asp:Panel ID="pnlCheque" runat="server" ScrollBars="Auto" Height="365px" BorderColor="Silver"
                                                    BorderStyle="Solid" BorderWidth="1px">
                                    <br />
                                    <asp:Repeater ID="rptCheque" runat="server" OnItemCommand="rptCheque_ItemCommand">
                                        <HeaderTemplate>
                                            <table style="width: 100%; margin-top: 0px">
                                                <tbody id="tblCheque">
                                                    <tr class="tblhead">
                                                        <td style="color: White;">
                                                            Customer
                                                        </td>
                                                        <td style="color: White;">
                                                            Cheque No
                                                        </td>
                                                        <td style="color: White;">
                                                            Bank Name
                                                        </td>
                                                        <td style="color: White;">
                                                            Cheque Date
                                                        </td>
                                                        <td style="color: White;">
                                                            Rec. Date
                                                        </td>
                                                        <td style="color: White;">
                                                            Deposit Date
                                                        </td>
                                                        <td style="color: White;">
                                                            Cheq. Amount
                                                        </td>
                                                        <td style="color: White;">
                                                            Slip No
                                                        </td>
                                                        <td style="color: White;" align="center">
                                                            Action
                                                        </td>
                                                    </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("Customer")%>
                                                    <asp:HiddenField ID="hfCHEQUE_PROCESS_ID" runat="server" />
                                                    <asp:HiddenField ID="hfCUSTOMER_ID" runat="server" />
                                                </td>
                                                <td>
                                                    <%# Eval("CHEQUE_NO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("BANK_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CHEQUE_DATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("RECEIVED_DATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DEPOSIT_DATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CHEQUE_AMOUNT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SlipNo")%>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="btnEdit" CommandArgument='<%# Eval("CHEQUE_PROCESS_ID")%>' CommandName="editcheque"
                                                        runat="server"  OnClientClick="javascript:return confirm('Are you sure you want to Edit?');return false;">
                                                            Edit
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr style="background-color: #E4E4E4">
                                                <td>
                                                    <asp:HiddenField ID="hfCHEQUE_PROCESS_ID" runat="server" />
                                                    <asp:HiddenField ID="hfCUSTOMER_ID" runat="server" />
                                                    <%# Eval("Customer")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CHEQUE_NO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("BANK_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CHEQUE_DATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("RECEIVED_DATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DEPOSIT_DATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CHEQUE_AMOUNT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SlipNo")%>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="btnEdit" CommandArgument='<%# Eval("CHEQUE_PROCESS_ID")%>' CommandName="editcheque"
                                                        runat="server"  OnClientClick="javascript:return confirm('Are you sure you want to Edit?');return false;">
                                                            Edit
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
