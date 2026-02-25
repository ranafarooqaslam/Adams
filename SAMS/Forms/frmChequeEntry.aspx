<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmChequeEntry.aspx.cs" Inherits="Forms_frmChequeEntry" Title="SAMS :: Cheque Entry"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("select").searchable();
        }

        function CalcChqAmount() {
            CalculateTax(1);
            CalculateChequeAmount();            
        }
        function ValidateForm() {
            var str;

            str = document.getElementById("<%= txtAmount.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must enter Amount');
                return false;
            }
            return true;
        }
        function Redirect() {
            window.location = "frmChequeEntryView.aspx?LevelID=87&LevelType=3";
        }

        function CheckReceivedAmount(checkbox, objCredit, objReceived, objBalance, objTaxAmount) {
          
            var credit = objCredit.value;
            var received = objReceived.value;
            objReceived.value = 0;
            if (parseFloat(objTaxAmount.value) > 0) {
                //Do nothing
            }
            else {
                objTaxAmount.value = 0;
            }
            if (checkbox.checked) {

                
                if (parseFloat(received) > 0) {
                    objReceived.value = 0;
                    objBalance.value = objCredit.value;
                }
                else {
                    objReceived.value = objCredit.value;
                }

                objBalance.value = 0;
            }
            else {
                
                objBalance.value = (parseFloat(objCredit.value.replace(',', '')) - parseFloat(objReceived.value.replace(',', ''))).toFixed(2);
            }
            if (parseInt(document.getElementById('<%=HFChqueProcessId.ClientID %>').value) > 0) {
                
                CalculateChequeAmount2();
            }
            else {

                CalculateChequeAmount();
            }
        }

        function CalculateChequeAmount2() {           
            var gv = document.getElementById('<%=gvInvoiceEdit.ClientID %>');
            var tb = gv.getElementsByTagName("input");
            var ChequeAmount = 0;
            var objValue = 0;
            for (var i = 0; i < tb.length; i++) {
                if (tb[i].type == "text") {
                    if (tb[i].name.indexOf("txtRcdAmount") !== -1) {                        
                        try {
                            tb[i + 3].value = (parseFloat(tb[i].value) * parseFloat(tb[i + 2].value) / 100).toFixed(2);
                            tb[i + 5].value = (parseFloat(tb[i -2].value) * parseFloat(tb[i + 4].value) / 100).toFixed(2);
                            tb[i + 6].value = (parseFloat(tb[i].value) - parseFloat(tb[i + 3].value) - parseFloat(tb[i + 5].value)).toFixed(2);
                            objValue = tb[i + 6].value;
                        }
                        catch (e) {

                        } 
                        if (parseFloat(objValue) > 0) {
                            ChequeAmount += parseFloat(objValue);                           
                        }
                    }
                }
            }

            //document.getElementById('<%=txtAmount.ClientID %>').value = (parseFloat(document.getElementById('<%=hfAmount.ClientID %>').value) + parseFloat(ChequeAmount)).toFixed(2);
            document.getElementById('<%=txtAmount.ClientID %>').value = (parseFloat(ChequeAmount)).toFixed(2);
        }

        function CalculateChequeAmount() {
            var gv = document.getElementById('<%=gvInvoice.ClientID %>');
            var tb = gv.getElementsByTagName("input");
            var ChequeAmount = 0;
            var objValue = 0;
            for (var i = 0; i < tb.length; i++) {
                if (tb[i].type == "text") {
                    if (tb[i].name.indexOf("txtRcdAmount") !== -1) {                        
                        try {                                                        
                            tb[i + 3].value = (parseFloat(tb[i].value) * parseFloat(tb[i + 2].value) / 100).toFixed(2);
                            tb[i + 5].value = (parseFloat(tb[i - 2].value) * parseFloat(tb[i + 4].value) / 100).toFixed(2);
                            tb[i + 6].value = (parseFloat(tb[i].value) - parseFloat(tb[i + 3].value) - parseFloat(tb[i + 5].value)).toFixed(2);                            
                            objValue = tb[i + 6].value;                            
                        }
                        catch (e) {

                        }
                        if (parseFloat(objValue) > 0) {
                            ChequeAmount += parseFloat(objValue);
                        }
                    }
                }
            }

            //document.getElementById('<%=txtAmount.ClientID %>').value = (parseFloat(document.getElementById('<%=hfAmount.ClientID %>').value) + parseFloat(ChequeAmount)).toFixed(2);
            document.getElementById('<%=txtAmount.ClientID %>').value = (parseFloat(ChequeAmount)).toFixed(2);
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = false;

        }


        function CalculateBalance(objCredit, objReceived, objBalance, objTaxAmount) {
            
            if (parseFloat(objReceived.value) > 0) {
                //Do nothing
            }
            else {
                objReceived.value = 0;

            }
            if (parseFloat(objTaxAmount.value) > 0) {
                //Do nothing
            }
            else {
                objTaxAmount.value = 0;
            }
            var BalanceAmount = (parseFloat(objCredit.value.replace(',', '')) - parseFloat(objReceived.value.replace(',', ''))).toFixed(2);
            objBalance.value = BalanceAmount;
            if (parseInt(document.getElementById('<%=HFChqueProcessId.ClientID %>').value) > 0) {
                CalculateChequeAmount2();
                CalculateTax2(0);
            }
            else {
                CalculateChequeAmount();
                CalculateTax(0);
            }
        }

        function SelectAllCheckboxesSpecific(spanChk) {
            var IsChecked = spanChk.checked;
            var Chk = spanChk;
            Parent = document.getElementById('gvInvoice');
            var items = Parent.getElementsByTagName('input');
            for (i = 0; i < items.length; i++) {
                if (items[i].id != Chk && items[i].type == "checkbox") {
                    if (items[i].checked != IsChecked) {
                        items[i].click();
                    }
                }
            }
        }
       
        function ChChequeListSelect(spanChk) {            
            var spanChk = document.getElementById('<%=ChbAllCatagories.ClientID %>');
            var IsChecked = spanChk.checked;           
            var Chk = spanChk;
            Parent = document.getElementById('<%= gvInvoice.ClientID %>');
            for (var i = 0, row; row = Parent.rows[i]; i++) {
                if (inputList = row.cells[0].getElementsByTagName("input")) {
                    for (var n = 0; n < inputList.length; n++) {
                        if (inputList[n].type == "checkbox") {
                            inputList[n].checked = IsChecked;
                        }
                    }
                }
                var Balance = 0.0;
                if (IsChecked) {
                    if (inputList = row.cells[7].getElementsByTagName("input")) {
                        if (inputList[0].type == "text") {                            
                            Balance = inputList[0].value;
                            var inputList2 = row.cells[4].getElementsByTagName("input");                            
                            inputList[0].value = parseFloat(inputList2[0].value).toFixed(2);                            
                        }
                    }

                    if (inputList = row.cells[6].getElementsByTagName("input")) {
                        if (inputList[0].type == "text") {
                            if (Balance > 0) {                              
                                inputList[0].value = Balance;
                            }
                            else {
                                
                            }
                        }
                    }

                    if (parseInt(document.getElementById('<%=HFChqueProcessId.ClientID %>').value) > 0) {
                        CalculateChequeAmount2();
                    }
                    else {
                        CalculateChequeAmount();
                        CalculateTax(1);
                    }
                }
                else {

                    document.getElementById('<%=txtAmount.ClientID %>').value = 0;
                    if (inputList = row.cells[6].getElementsByTagName("input")) {
                        if (inputList[0].type == "text") {
                            Balance = inputList[0].value;                            
                        }
                    }
                    if (inputList = row.cells[7].getElementsByTagName("input")) {
                        if (inputList[0].type == "text") {
                            if (Balance > 0) {
                                inputList[0].value = Balance;
                            }
                            else {
                                inputList[0].value = 0;
                            }
                        }
                    }
                }
            }                       
                      
        }

        function CalculateTax(ObjType) {

            var amnt = 0.0;
            var amnt2 = 0.0;
            var Tax = 0.0;
            var Tax2 = 0.0;
            var str = 0.0;
            var str2 = 0.0;
            str = document.getElementById("<%= txtTaxPer.ClientID %>").value;
            str2 = document.getElementById("<%= txtSalesTaxPer.ClientID %>").value;
            if (str > 0) {

            } else {
                str = 0;
            }
            if (str2 > 0) {

            } else {
                str2 = 0;
            }
            var grid = document.getElementById("<%=gvInvoice.ClientID%>");
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "text") {
                    if (inputs[i].name.indexOf("txtRcdAmount") !== -1) {
                        if (ObjType == 1) {
                            inputs[i + 2].value = str;
                            inputs[i + 4].value = str2;
                        }
                        amnt = parseFloat(inputs[i].value);                        
                        amnt2 = parseFloat(inputs[i - 2].value);                        
                        Tax = parseFloat(inputs[i + 2].value);
                        Tax2 = parseFloat(inputs[i + 4].value);
                        if (amnt > 0 && (Tax > 0 || Tax2 > 0)) {
                            inputs[i + 3].value = (amnt * Tax / 100).toFixed(2);
                            inputs[i + 5].value = (amnt2 * Tax2 / 100).toFixed(2);
                            inputs[i + 6].value = parseFloat(amnt - inputs[i + 3].value - inputs[i + 5].value).toFixed(2);                            
                        } 
                        else {
                            inputs[i + 3].value = 0;
                            inputs[i + 5].value = 0;
                            inputs[i + 6].value = amnt.toFixed(2);
                        }
                    }
                }
            }
        }

        function CalculateTax2(ObjType) {
            var amnt2 = 0.0;
            var amnt3 = 0.0;
            var Tax2 = 0.0;            
            var Tax3 = 0.0;
            var str = 0.0;
            str = document.getElementById("<%= txtTaxPer.ClientID %>").value;
            str2 = document.getElementById("<%= txtSalesTaxPer.ClientID %>").value;

            if (str2 > 0) {

            } else {
                str2 = 0;
            }
            if (str3 > 0) {

            } else {
                str2 = 0;
            }            
            
            var grid2 = document.getElementById("<%=gvInvoiceEdit.ClientID%>");
            var inputs2 = grid2.getElementsByTagName("input");
            for (var i = 0; i < inputs2.length; i++) {
                if (inputs2[i].type == "text") {

                    if (inputs2[i].name.indexOf("txtRcdAmount") !== -1) {
                        if (ObjType == 1) {
                            inputs[i + 2].value = str2;
                            inputs[i + 4].value = str3;
                        }

                        amnt2 = parseFloat(inputs[i].value);
                        amnt3 = parseFloat(inputs[i - 2].value);
                        Tax2 = parseFloat(inputs[i + 2].value);
                        Tax3 = parseFloat(inputs[i + 4].value);

                        if ((amnt2 > 0 && Tax2 > 0) || (amnt3 > 0 && Tax3 > 0)) {
                            inputs[i + 3].value = (amnt2 * Tax2 / 100).toFixed(2);
                            inputs[i + 5].value = (amnt3 * Tax3 / 100).toFixed(2);
                            inputs[i + 6].value = parseFloat(amnt2 - inputs[i + 3].value - inputs[i + 5].value).toFixed(2);
                        }
                        else {
                            inputs[i + 3].value = 0;
                            inputs[i + 5].value = 0;
                            inputs[i + 6].value = amnt2.toFixed(2);
                        }

                    }
                }
            }
        }       
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                            <ContentTemplate>                            
                                <table width="100%" cellpadding="3">
                                    <tr>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="lblStatus" runat="server" Text="Cheque Status"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:35%">
                                            <asp:DropDownList ID="DrpStatus" runat="server" Width="220px" 
                                                onselectedindexchanged="DrpStatus_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="lblHeadOffice" runat="server" Text="H.O. Account Code"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:35%">
                                            <asp:DropDownList ID="ddlHeadOffice" runat="server" Width="220px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="lblChequeNo" runat="server" Text="Cheque No"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:35%">
                                            <asp:TextBox ID="txtChequeNo" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="lblSalesTax" runat="server" Text="S. Tax Debit A.C"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:35%">
                                            <asp:DropDownList ID="ddlSalesTax" runat="server" Width="220px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="lblChequeAmount" runat="server" Text="Chq Amount"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:35%">
                                            <asp:TextBox ID="txtAmount" runat="server" Width="220px" ></asp:TextBox>
                                            <asp:HiddenField ID="hfAmount" runat="server" Value="0" />
                                        </td>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="lblDebit" runat="server" Text="Tax Account Debit Head"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:35%">
                                            <asp:DropDownList ID="DrpAccountHead" runat="server" Width="220px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="lblBank" runat="server" Text="Bank Name"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:35%">
                                            <asp:TextBox ID="txtBankName" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="lblDeposit" runat="server" Text="Deposit Account"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:35%">
                                            <asp:DropDownList ID="DrpBankAccount" runat="server" Width="220px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="lblChequeDate" runat="server" Text="Cheque Date"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:35%">
                                            <asp:TextBox ID="txtStartDate" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="lblSaleForce" runat="server" Text="Sale Force"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:35%">
                                            <asp:DropDownList ID="DrpDeliveryMan" runat="server" Width="220px">
                                            </asp:DropDownList>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="lblReceiveDate" runat="server" Text="Recevied Date"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:35%">
                                            <asp:TextBox ID="txtReceivedDate" runat="server" Width="220px" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="lblTax" runat="server" Text="W.H.Income Tax %"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:35%">
                                            <asp:TextBox ID="txtTaxPer" runat="server" Width="220px" onkeyup="CalcChqAmount();">
                                             </asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExender322" runat="server"
                                                FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtTaxPer">
                                            </ajaxToolkit:FilteredTextBoxExtender>                                                                                                                    
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="lblSlipNo" runat="server" Text="Slip No"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:35%">
                                            <asp:TextBox ID="txtSlipNo" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="lblSalesTaxPer" runat="server" Text="W.H. Sales Tax %"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:35%">
                                            <asp:TextBox ID="txtSalesTaxPer" runat="server" Width="220px" onkeyup="CalcChqAmount();">
                                             </asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtSalesTaxPer">
                                            </ajaxToolkit:FilteredTextBoxExtender>  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Text="Remarks"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 85%" colspan="3">
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="86%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:CheckBox ID="ChbAllCatagories" runat="server" Text="Select All" 
                                    onclick="ChChequeListSelect(this);">
                                </asp:CheckBox>
                                <table width="100%">
                                    <tr class="tblhead" align="center" valign="middle">
                                        <th align="left" scope="col" style="height: 25px; width: 2%;">
                                            
                                        </th>
                                        <th align="left" scope="col" style="height: 25px; width: 13%;">
                                            Customer
                                        </th>
                                        <th align="left" scope="col" style="height: 25px; width: 8%;">
                                            Invoice No
                                        </th>
                                        <th align="left" scope="col" style="height: 25px; width: 8%;">
                                            Invoice Date
                                        </th>
                                        <th align="left" scope="col" style="height: 25px; width: 8%;">
                                            Opening Amount
                                        </th>
                                        <th align="left" scope="col" style="height: 25px; width: 4%;">
                                            Inv. <br />S. Tax
                                        </th>
                                        <th align="left" scope="col" style="height: 25px; width: 8%;">
                                            Received Amount
                                        </th>
                                        <th align="left" scope="col" style="height: 25px; width: 8%;">
                                            Current Received
                                        </th>
                                        <th align="left" scope="col" style="height: 25px; width: 8%;">
                                            Balance Amount
                                        </th>
                                        <th class="HidePanel" scope="col" style="height: 25px; width: 8%;">
                                            DELIVERYMAN_ID
                                        </th>
                                        <th align="left" scope="col" style="height: 25px; width: 4%;">
                                            W.H.I. Tax %
                                        </th>
                                        <th align="left" scope="col" style="height: 25px; width: 8%;">
                                            W.H.I. Tax Amount
                                        </th>
                                        <th align="left" scope="col" style="height: 25px; width: 4%;">
                                            W.H.S. Tax %
                                        </th>
                                        <th align="left" scope="col" style="height: 25px; width: 8%;">
                                            W.H.S. Tax Amount
                                        </th>
                                        <th align="left" scope="col" style="height: 25px; width: 10%;">
                                            Cheque Amount
                                        </th>
                                    </tr>
                                </table>
                                <asp:Panel ID="pnlInvoice" runat="server" Height="200px" ScrollBars="Vertical" BorderColor="Silver"
                                    BorderStyle="Solid" BorderWidth="1px" Width="100%">
                                    <asp:GridView ID="gvInvoice" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center" Width="100%"
                                        DataKeyNames="SALE_INVOICE_ID" OnRowDataBound="gvInvoice_RowDataBound" ShowHeader="False" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChbIsAssigned" runat="server" Checked='<%#Eval("is_Checked")%>'/>
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="2%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="13%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MANUAL_INVOICE_ID" HeaderText="Invoice No">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="8%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Invoice Date">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="8%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Opening Amount">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="8%"  />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCrdAmount" runat="server" Width="100%" Text='<%#(Convert.ToDecimal( Eval("CURRENT_CREDIT_AMOUNT")) - Convert.ToDecimal(Eval("RCDAMOUNT"))).ToString("0.00")%>'
                                                        ReadOnly="true">                                                 
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sales Tax">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="4%"/>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtInvoiceSalesTax" runat="server" Width="100%" Text='<%#(Convert.ToDecimal(Eval("InvoiceTax"))).ToString("0.00")%>'
                                                        ReadOnly="true">                                                 
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Received Amount">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="8.5%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRecievedAmount" runat="server" Width="100%" Text='<%#Convert.ToDecimal(Eval("ReceivedAMOUNT"))%>'
                                                        ReadOnly="true">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Received">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="8%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRcdAmount" runat="server" Width="100%" Text="0" OnTextChanged="txtRcdAmount_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextoxExtender10" runat="server" FilterType="Numbers, Custom"
                                                     ValidChars="." TargetControlID="txtRcdAmount" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance Amount">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="8.5%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBalAmount" runat="server" Width="100%" ReadOnly="true" Text='<%#(Convert.ToDecimal( Eval("CURRENT_CREDIT_AMOUNT")) - Convert.ToDecimal(Eval("RCDAMOUNT")) ).ToString("0.00")%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DELIVERYMAN_ID" HeaderText="DELIVERYMAN_ID">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CUSTOMER_ID" HeaderText="CUSTOMER_ID">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CHANNEL_TYPE_ID" HeaderText="CHANNEL_TYPE_ID">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="W.H. Tax %">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="4%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTax" runat="server" Text="0" Width="100%" OnTextChanged="txtTaxInvoice_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="W.H. Tax Amount">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="8%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTaxAmount"  ReadOnly="true" runat="server" Text="0" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sales Tax %">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="4%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSalesTax" runat="server" Text="0" Width="100%" OnTextChanged="txtTaxInvoice_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S. Tax Amount">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="8%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSalesTaxAmount"  ReadOnly="true" runat="server" Text="0" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField HeaderText="Cheque Amount">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="10%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtChequeAmountTax" runat="server" Width="100%" Text="0"></asp:TextBox>                                                 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RowID" HeaderText="Row">
                                                 <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>
                                    <asp:GridView ID="gvInvoiceEdit" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center" Width="100%"
                                        DataKeyNames="SALE_INVOICE_ID" OnRowDataBound="gvInvoice_RowDataBound" ShowHeader="false" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChbIsAssigned" runat="server"/>
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="2%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="13%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MANUAL_INVOICE_ID" HeaderText="Invoice No">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="8%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Invoice Date">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="8%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Opening Amount">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="8%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCrdAmount" runat="server" Width="100%" Text='<%#(Convert.ToDecimal( Eval("CURRENT_CREDIT_AMOUNT")) - Convert.ToDecimal(Eval("RCDAMOUNT"))).ToString("0.00")%>'
                                                    ReadOnly="true">                                                                    
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sales Tax">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="4%"/>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtInvoiceSalesTax" runat="server" Width="100%" Text='<%#(Convert.ToDecimal(Eval("InvoiceTax"))).ToString("0.00")%>'
                                                        ReadOnly="true">                                                 
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Received Amount">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="8.5%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRecievedAmount" runat="server" Width="100%" Text='<%#Convert.ToDecimal(Eval("ReceivedAMOUNT")).ToString("0.00")%>' ReadOnly="true">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Received">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="8%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRcdAmount" runat="server" Width="100%" Text='<%#Eval("CurrentReceivedAmount")%>'></asp:TextBox>
                                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9"  runat="server" FilterType="Numbers, Custom"
                                                        ValidChars="." TargetControlID="txtRcdAmount" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance Amount">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="8.5%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBalAmount" runat="server" Width="100%" ReadOnly="true" Text='<%#(Convert.ToDecimal( Eval("CURRENT_CREDIT_AMOUNT")) - Convert.ToDecimal(Eval("RCDAMOUNT"))-Convert.ToDecimal(Eval("CurrentReceivedAmount"))).ToString("0.00")%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DELIVERYMAN_ID" HeaderText="DELIVERYMAN_ID">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CUSTOMER_ID" HeaderText="CUSTOMER_ID">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CHANNEL_TYPE_ID" HeaderText="CHANNEL_TYPE_ID">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="W.H. Tax %">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="4%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTax" runat="server" Width="100%" OnTextChanged="txtTaxInvoice_TextChanged" AutoPostBack="True" Text='<%#(Convert.ToDecimal( Eval("TAX"))).ToString("0.00")%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="W.H. Tax Amount">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="8%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTaxAmount"  ReadOnly="true" runat="server" Text='<%#(Convert.ToDecimal( Eval("TAX_AMOUNT"))).ToString("0.00")%>' Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sales. Tax %">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="4%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSalesTax" runat="server" Text='<%#(Convert.ToDecimal( Eval("TAX2"))).ToString("0.00")%>' Width="100%" OnTextChanged="txtTaxInvoice_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S. Tax Amount">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="8%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSalesTaxAmount"  ReadOnly="true" runat="server" Text='<%#(Convert.ToDecimal( Eval("TAX_AMOUNT2"))).ToString("0.00")%>' Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField HeaderText="Cheque Amount">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                    Width="10%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtChequeAmountTax" runat="server" Width="100%" Text='<%#Convert.ToDecimal(Eval("CHEQUE_AMOUNT_TAX")).ToString("0.00") %>'></asp:TextBox>                                               
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SALE_INVOICE_ID" HeaderText="SALE_INVOICE_ID">
                                                <ItemStyle CssClass="HidePanel"/>
                                                <HeaderStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>
                                </asp:Panel>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table width="100%">
                                            <tr>
                                                <td align="right" width="56%">
                                                        <asp:LinkButton ID="linkbtnprev" runat="server" 
                                                   onclick="linkbtnprev_Click" CausesValidation="false" Visible="false" style="color: #6C5A10;">Previous</asp:LinkButton>
                                                        &nbsp;<asp:LinkButton ID="linkbtnnext" runat="server" 
                                                  onclick="linkbtnnext_Click" CausesValidation="false" Visible="false" style="color: #6C5A10;">Next</asp:LinkButton>
                                                </td>
                                                <td align="right" width="44%">
                                                <asp:Label ID="lblCurrentPageNo" runat="server" Text="" Visible="false" style="color: #6C5A10;"></asp:Label>
                                                    <asp:Label ID="lblOf" runat="server" Text="Of" Visible="false" style="color: #6C5A10;"></asp:Label>
                                                    <asp:Label ID="lblTotalNoOfPages" runat="server" Text="" Visible="false" style="color: #6C5A10;"></asp:Label>
                                                    <asp:Label ID="lblDummy" runat="server" Text="| Total Records:" Visible="false" style="color: #6C5A10;"></asp:Label>
                                                    <asp:Label ID="lblTotalNoOfRecords" runat="server" Text="" Visible="false" style="color: #6C5A10;"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:Button AccessKey="S" ID="btnSave" OnClick="btnSave_Click2" runat="server" Width="102px"
                                                    Font-Size="8pt" Text="Save" CssClass="Button" CausesValidation="true" UseSubmitBehavior="true"  />
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                                    Text="Cancel" OnClick="btnCancel_Click" CssClass="Button" />
                                            </td>
                                            <td style="width: 100px" valign="top" align="left">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtAmount">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtStartDate"
                                    Mask="99/99/9999" MaskType="Date">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:HiddenField ID="HFChqueProcessId" runat="server"></asp:HiddenField>
                            </ContentTemplate>
                            <Triggers>                                
                             <asp:PostBackTrigger ControlID="btnSave"  />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>

               <div style="z-index: 101; left:380px; width: 100px; position:absolute; top:180px; height: 100px">
                            <asp:Panel ID="Panel21" runat="server"> 
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                      <div id='messagediv' style="text-align:center">
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="27px" />
                                        Wait Update.......
                                        </div>
                                         </ProgressTemplate>
                                       
                                </asp:UpdateProgress>

                            </asp:Panel>
                             
                              </div>
        </div>
    </div>
</asp:Content>
