<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmVehicleMaintenance.aspx.cs" Inherits="Forms_frmVehicleMaintenance"
    Title="SAMS :: Vehicle Maintenance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">    
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
 function pageLoad() {
          $("select").searchable();
        }
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtAmount.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Amount');
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

      
        function Highlight() {
     
            var srchval = document.getElementById('<%= txtSearch.ClientID %>').value;
            var cntrlname = document.getElementById('<%= GrdMaintenance.ClientID %>');
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
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="70%">
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
                                        <td style="width: 86px">
                                            <strong>
                                                <asp:Label ID="lblCredit" runat="server" Height="14px" Text="Credit" Width="88px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpCreditTo" runat="server" Width="315px" Visible="true">
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
                                        <td style="width: 86px">
                                            <strong>
                                                <asp:Label ID="lblCreditFrom" runat="server" Height="14px" Text="Debit" Width="79px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpCreditFrom" runat="server" Width="315px" Visible="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30px; width: 20px;">
                                            <strong>
                                                <asp:Label ID="lblFuel" runat="server" Height="14px" Text="Type" Width="98px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DrpFuelType" runat="server" AutoPostBack="True" Width="200px"
                                                OnSelectedIndexChanged="DrpFuelType_SelectedIndexChanged">
                                                <asp:ListItem Value="1">Food</asp:ListItem>
                                                <asp:ListItem Value="2">Toll</asp:ListItem>
                                                <asp:ListItem Value="3">Maintenance</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
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
                                        <td style="height: 30px;">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Text="Driver" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DrpDriver" runat="server" AutoPostBack="True" CssClass="DropList"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Text="Vehicle Reading" Width="94px"></asp:Label></strong>
                                        </td>
                                        <td valign="middle" align="left" style="height: 30px;">
                                            <asp:TextBox ID="txtVehicleReading" runat="server" CssClass="txtBox" Width="195px"
                                                Height="16px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="ftbeVehicleReading" runat="server" FilterType="Numbers, Custom"
                                                             ValidChars="." TargetControlID="txtVehicleReading">
                                                 </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="height: 30px;">
                                            <strong>
                                                <asp:Label ID="Label7" runat="server" Text="Loader" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DrpLoader" runat="server" AutoPostBack="True" CssClass="DropList"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30px;">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Text="Amount" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="txtBox" Width="195px" onkeydown="return jsDecimals(event);"></asp:TextBox>
                                        </td>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label9" runat="server" Text="Document Date" Width="94px"></asp:Label></strong>
                                        </td>
                                        <td align="left" style="width: 238px; height: 25px">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="txtBox" MaxLength="10" Width="180px"
                                                AutoPostBack="true" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
                                            <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                PopupButtonID="ImgBntToDate" TargetControlID="txtToDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30px;">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Text="Remarks" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtBox" Width="645px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="119px" Font-Size="8pt"
                                                Text="Save" CssClass="Button" OnClick="btnSaveDocument_Click" />
                                            <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                                Text="Cancel" CssClass="Button" OnClick="btnCancel_Click1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="border: thin inset silver; width: 750px; background-color: silver">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 61px">
                                                            <asp:Label ID="Label1" runat="server" Width="48px" Text="Search" Height="18px"></asp:Label>
                                                        </td>
                                                        <td style="width: 170px; height: 21px" align="left">
                                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="txtBox" Width="150px" onkeyup="Highlight()"></asp:TextBox>
                                                        </td>
                                                        <td style="height: 21px" align="left" width="100">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="10">
                                            <asp:Panel ID="Panel2" runat="server" Width="750px" Height="230px" ScrollBars="Vertical"
                                                BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                <asp:GridView ID="GrdMaintenance" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                    BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                    OnRowDeleting="GrdMaintenance_RowDeleting" OnRowCommand="GrdMaintenance_RowCommand"
                                                    ShowHeader="true" OnRowDataBound="GrdMaintenance_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="VEHICLE_MAINTENANCE_ID" HeaderText="VM_ID">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MAINTENANCE_TYPE" HeaderText="MT">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="VEHICLE_ID" HeaderText="Vehicle_ID">
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
                                                                Width="150px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="VEHICLE_READING" HeaderText="Reading">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="left"
                                                                Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="AMOUNT" HeaderText="Amount" DataFormatString="{0:F2}">
                                                            <ItemStyle BorderColor="Silver" Width="100px" BorderStyle="Solid" BorderWidth="2px"
                                                                HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="remarks" HeaderText="Remarks">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="left"
                                                                Width="200px" />
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
                                                        <asp:BoundField DataField="principal_id" HeaderText="Quantity">
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
                                                    <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="black" HorizontalAlign="Left"
                                                        VerticalAlign="Middle" />
                                                </asp:GridView>
                                            </asp:Panel>
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
    </div>
</asp:Content>
