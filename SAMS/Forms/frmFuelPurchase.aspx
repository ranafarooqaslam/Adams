<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmFuelPurchase.aspx.cs" Inherits="Forms_frmFuelPurchase" Title="SAMS :: Fuel Purchase" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function ValidateForm() {

            var str;

            str = document.getElementById('<%=txtQty.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }
            str = document.getElementById('<%=txtRate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Rate');
                return false;
            }

            return true;
        }


        function pageLoad() {

            $("select").searchable();
        }

        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }


        function jsDecimals(e) {

            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                    if (!jsIsUserFriendlyChar(key, "Decimals")) {
                        return false;
                    }
                }
                else {
                    if (evt.shiftKey) {
                        return false;
                    }
                }
            }
            return true;
        }
        function jsIsUserFriendlyChar(val, step) {

            // Backspace, Tab, Enter, Insert, and Delete  
            if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
                return true;
            }
            // Ctrl, Alt, CapsLock, Home, End, and Arrows  
            if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
                return true;
            }
            if (step == "Decimals") {
                if (val == 190 || val == 110) {  //Check dot key code should be allowed
                    return true;
                }
            }
            // The rest  
            return false;
        }

        function CalculateAmount() {

            var Rate = document.getElementById("<%= txtRate.ClientID %>").value;
            document.getElementById("<%= txtAmount.ClientID %>").value = document.getElementById("<%= txtQty.ClientID %>").value * Rate;
            return true;

        }


        function abc(e) {

            var Rate = document.getElementById("<%= txtRate.ClientID %>").value;
            document.getElementById("<%= txtAmount.ClientID %>").value = document.getElementById("<%= txtQty.ClientID %>").value * Rate;
            return true;
        }
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }

    </script>
     <script type="text/javascript">
         function showPopup() {
             var modalPopupBehavior = $find('programmaticModalPopupBehavior');
             modalPopupBehavior.show();
         }
         function hidepopup() {
             var modalPopupBehavior = $find('programmaticModalPopupBehavior');
             modalPopupBehavior.hide();
         }
    </script>
 <style type="text/css" >
            .modalBackground {
 background-color:Gray;
 filter:alpha(opacity=70);
 opacity:0.7;

}

.modalPopup {
 background-color:#ffffdd;
 border-width:3px;
 border-style:solid;
 border-color:Gray;
 padding:3px;
 width:350px;
}
    </style>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label>
                                             </strong>
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="width: 20px;">
                                            <strong>
                                                <asp:Label ID="lblCredit" runat="server" Height="14px" Text="Credit" Width="79px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpCreditTo" runat="server" Width="200px" Visible="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30px; width: 20px;">
                                            <strong>
                                                <asp:Label ID="lblFuel" runat="server" Height="14px" Text="Fuel Type" Width="98px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DrpFuelType" runat="server" AutoPostBack="True" Width="200px"
                                                OnSelectedIndexChanged="DrpFuelType_SelectedIndexChanged">
                                                <asp:ListItem Value="1">Petrol</asp:ListItem>
                                                <asp:ListItem Value="2">Deisel</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>
                                                <asp:Label ID="lblCreditFrom" runat="server" Height="14px" Text="Debit" Width="81px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpCreditFrom" runat="server" Width="200px" Visible="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                       <td align="left">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Text="Date" Width="94px"></asp:Label></strong>
                                        </td>
                                        <td valign="middle" align="left" style="width: 230px; height: 30px;">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="txtBox" MaxLength="10" Width="175px"
                                                Height="16px" AutoPostBack ="true" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
                                            <%@register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                            <cc1:calendarextender ID="CalendarExtender2" runat="server" EnableViewState="false"
                                                Format="dd-MMM-yyyy" PopupButtonID="ImgBntToDate" TargetControlID="txtToDate"
                                                 OnClientShown="calendarShown"  >
                                            </cc1:calendarextender>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                         <td style="height: 30px;">
                                            <strong>
                                                <asp:Label ID="lblDocumentNo" runat="server" Text="Document No" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" 
                                                Width="200px" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30px;">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="Qty in Ltr" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="txtBox" Width="195px" onkeyup="return abc(event);"
                                                onkeydown="return jsDecimals(event);"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30px;">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Text="Rate" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRate" runat="server" CssClass="txtBox" Width="195px" onkeyup="return abc(event);"
                                                onkeydown="return jsDecimals(event);"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30px;">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Text="Amount" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="txtBox" Width="195px" onfocus="CalculateAmount()"
                                                Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                             <Triggers>
                <asp:PostBackTrigger ControlID="btnSaveDocument" />
            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="119px" Font-Size="8pt"
                                    Text="Save" CssClass="Button" OnClick="btnSaveDocument_Click" OnClientClick="showPopup()" />
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                    Text="Cancel" CssClass="Button" OnClick="btnCancel_Click" />
                                <strong>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    
                   </td>
                </tr>
            </table>
                <div style="z-index: 101; left: 597px; width: 100px; position: absolute; top: 209px;
                            height: 100px">
                            <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup" style="background-color:#FFFFCC;display:none;height:50px;width:85px;padding:10px">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                   
                                    <ProgressTemplate>
                                      <div id='messagediv' style="text-align:center">
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="27px" />
                                        Wait Update.......
                                        </div>
                                         </ProgressTemplate>
                                       
                                </asp:UpdateProgress>
                                 


                            </asp:Panel>
                           <cc1:ModalPopupExtender runat="server" ID="programmaticModalPopup"
            BehaviorID="programmaticModalPopupBehavior"
            TargetControlID="hiddenTargetControlForModalPopup"
            PopupControlID="programmaticPopup"
            BackgroundCssClass="modalBackground"
            DropShadow="True"
            RepositionMode="RepositionOnWindowScroll" >
        </cc1:ModalPopupExtender>
          <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" style="display:none"/>
                        </div>
        </div>
    </div>
</asp:Content>
