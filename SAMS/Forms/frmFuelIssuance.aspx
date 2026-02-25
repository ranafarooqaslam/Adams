<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmFuelIssuance.aspx.cs" Inherits="Forms_frmFuelIssuance" Title="SAMS :: Fuel Issuance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                alert('Must Select Stock / Rate');
                return false;
            }
            return true;
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

        String.prototype.startsWith = function (str) { return (this.indexOf(str) === 0); }
        function Highlight() {
            var srchval = document.getElementById('<%= txtSearch.ClientID %>').value;
            var cntrlname = document.getElementById('<%= GrdFuelIssuance.ClientID %>');
            var rows = cntrlname.getElementsByTagName("tr");
            var rowTop = 0;
            var FirstRow = rows[1];
            for (var loop = 1; loop < rows.length; loop++) {
                rows[loop].style.background = '#ffffff';
                rows[loop].style.visibility = 'visible';

            }
            if (srchval != '') {
                for (var loop = 1; loop < rows.length; loop++) {
                    var ok = 0;
                    var cells = rows[loop].getElementsByTagName("td");
                    for (i = 0; i < cells.length; i++) {
                        if (cells[i].innerHTML.toLowerCase().startsWith(srchval.toLowerCase())) {
                            ok = 1;
                        }
                    }
                    if (ok == 1) {
                        CurrentRow = rows[loop];
                        SendUp(CurrentRow, FirstRow);
                        CurrentRow.style.background = '#eae9e9';
                    }
                    else
                    //  rows[loop].style.background = '#ffffff';
                        rows[loop].style.visibility = 'hidden';


                }
            }
            return false;
        }
        function SendUp(CurrentRow, FirstRow) {
            FirstRow.parentNode.insertBefore(CurrentRow, FirstRow);
        }
        function GetMedName(MedName) {
            document.getElementById('<%= txtSearch.ClientID %>').value = MedName;
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
        function pageLoad() {
            $("select").searchable();
            $("input:text").keypress(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        }
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
    <div id="right_data">
        <div style="z-index: 101; left: 697px; width: 100px; position: absolute; top: 159px;
            height: 100px">
            <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup" Style="background-color: #FFFFCC;
                display: none; height: 50px; width: 85px; padding: 10px">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatepanel">
                    <ProgressTemplate>
                        <div id='messagediv' style="text-align: center">
                            <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                Width="27px" />
                            Wait Update.......
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
            <cc1:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
                TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="programmaticPopup"
                BackgroundCssClass="modalBackground" DropShadow="True" RepositionMode="RepositionOnWindowScroll">
            </cc1:ModalPopupExtender>
            <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
        </div>
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
                                            <asp:Label ID="lblCredit" runat="server" Height="14px" Text="Credit" Width="98px"></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpCreditTo" runat="server" Width="300px" Visible="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label8" runat="server" Width="78px" Text="Principal" CssClass="lblbox"></asp:Label></strong>
                                    </td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                            AutoPostBack="True" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblCreditFrom" runat="server" Height="14px" Text="Debit" Width="98px"></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpCreditFrom" runat="server" Width="300px" Visible="true">
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
                                    <td rowspan="6" colspan="2">
                                        <strong>
                                            <asp:Label ID="Label2" runat="server" Text="Inventroy Detail" Width="94px"></asp:Label>
                                        </strong>
                                        <asp:Panel ID="panel" Width="300px" runat="server" Height="150px" BorderColor="Black"
                                            BorderWidth="1px" ScrollBars="Auto">
                                            <asp:GridView ID="Grdfuel" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center" Width="100%"
                                                DataKeyNames="Price" 
                                                OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged">
                                                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                    PreviousPageText="Previous" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="chbStockRate" runat="server" Width="20px" AutoPostBack="true"
                                                                runat="server" OnCheckedChanged="chbStockRate_CheckedChanged" />
                                                        </ItemTemplate>
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Price" HeaderText="Rate" DataFormatString="{0:n2}">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="200px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Quantity" HeaderText="Stock in Ltr" DataFormatString="{0:n2}">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="200px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 30px;">
                                        <strong>
                                            <asp:Label ID="Label6" runat="server" Text="Vehicle" Width="94px"></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DrpVehicleNo" runat="server" AutoPostBack="True" CssClass="DropList"
                                            Width="200px" OnSelectedIndexChanged="DrpVehicleNo_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 30px;">
                                        <strong>
                                            <asp:Label ID="lblSalePerson" runat="server" Text="Sales Person" Width="94px"></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DrpSalesPerson" runat="server" AutoPostBack="True" CssClass="DropList"
                                            Width="200px" OnSelectedIndexChanged="DrpSalesPerson_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label9" runat="server" Text="Date" Width="94px"></asp:Label></strong>
                                    </td>
                                    <td valign="middle" align="left" style="width: 230px; height: 30px;">
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="txtBox" MaxLength="10" Width="175px"
                                            Height="16px" AutoPostBack="true" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                                        <asp:ImageButton ID="ImgBntToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" EnableViewState="false"
                                            Format="dd-MMM-yyyy" PopupButtonID="ImgBntToDate" TargetControlID="txtToDate"
                                            OnClientShown="calendarShown">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label5" runat="server" Text="Vehicle Reading" Width="94px"></asp:Label></strong>
                                    </td>
                                    <td valign="middle" align="left" style="width: 230px; height: 30px;">
                                        <asp:TextBox ID="txtVehicleReading" runat="server" CssClass="txtBox" Width="195px"
                                            Height="16px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ftbeVehicleReading" runat="server" FilterType="Numbers, Custom"
                                                     ValidChars="." TargetControlID="txtVehicleReading">
                                         </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 30px;">
                                        <strong>
                                            <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="Qty in Ltr" Width="94px"></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty" runat="server" onkeyup="return CalculateAmount(event);"
                                            CssClass="txtBox" Width="195px" onkeydown="return jsDecimals(event);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 30px;">
                                        <strong>
                                            <asp:Label ID="Label3" runat="server" Text="Rate" Width="94px"></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRate" runat="server" CssClass="txtBox" Width="195px" onkeyup="return CalculateAmount(event);"
                                            onkeydown="return jsDecimals(event);" ReadOnly="true"></asp:TextBox>
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
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                        <asp:PostBackTrigger ControlID="txtToDate" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="119px" Font-Size="8pt"
                                    Text="Save" CssClass="Button" OnClick="btnSaveDocument_Click" OnClientClick="showPopup()" />
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                    Text="Cancel" CssClass="Button" OnClick="btnCancel_Click1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="updatepanel" runat="server">
                                    <ContentTemplate>
                                        <table style="border: thin inset silver; width: 750px; background-color: silver">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 50px; height: 21px" align="left">
                                                        <asp:Label ID="Label7" runat="server" Width="48px" Text="Search" Height="18px"></asp:Label>
                                                    </td>
                                                    <td style="width: 170px; height: 21px" align="left">
                                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txtBox" Width="150px" onkeyup="Highlight()"></asp:TextBox>
                                                    </td>
                                                    <td style="height: 21px" align="left" width="100">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        </td> </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel2" runat="server" Width="750px" Height="230px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdFuelIssuance" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        OnRowDeleting="GrdFuelIssuance_RowDeleting" OnRowCommand="GrdFuelIssuance_RowCommand"
                                                        ShowHeader="true">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField DataField="FUEL_ISSUANCE_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="FUEL_TYPE" HeaderText="SKU Code">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="VEHICLE_ID" HeaderText="SKU Name">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SALE_PERSON_ID" HeaderText="Quantity">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Vehicle_No" HeaderText="Vehicle">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="left"
                                                                    Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="User_Name" HeaderText="Sale Person">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="left"
                                                                    Width="200px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="VEHICLE_READING" HeaderText="Reading">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="left"
                                                                    Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY" HeaderText="Qty" DataFormatString="{0:F2}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="left"
                                                                    Width="150px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PRICE" HeaderText="Rate" DataFormatString="{0:F2}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="left"
                                                                    Width="150px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="AMOUNT" HeaderText="Amount" DataFormatString="{0:F2}">
                                                                <ItemStyle BorderColor="Silver" Width="150px" BorderStyle="Solid" BorderWidth="2px"
                                                                    HorizontalAlign="left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CREDIT_TO" HeaderText="Quantity">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CREDIT_FROM" HeaderText="Quantity">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Distributor_Id" HeaderText="Distributor">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Principal_id" HeaderText="Quantity">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
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
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" Width="45px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="White" />
                                                        <PagerStyle BackColor="Transparent" />
                                                        <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                                            VerticalAlign="Middle" />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
        </table>
    </div>
</asp:Content>
