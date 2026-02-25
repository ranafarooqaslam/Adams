<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmVehicleAssingment.aspx.cs" Inherits="Forms_frmVehicleAssingment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <asp:UpdatePanel ID="pnl_head" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpDistributor" runat="server" Width="180px" CssClass="DropList"
                                AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                         <td>
                            <strong>
                                <asp:Label ID="Label11" runat="server" Width="81px" Text="Designation" CssClass="lblbox"></asp:Label></strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="DrpDesignation" runat="server" Width="180px" CssClass="DropList"
                                AutoPostBack="True" OnSelectedIndexChanged="DrpDesignation_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="Label1" runat="server" Text="Vehicle No" CssClass="label"></asp:Label></strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="DrpVehicleno" runat="server" Width="180px" CssClass="DropList"
                                AutoPostBack="True" OnSelectedIndexChanged="DrpVehicleno_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 10px;">
                        </td>
                       
                         <td>
                            <strong>
                                <asp:Label ID="Label6" runat="server" Text="Assign To"></asp:Label></strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="DrpAssignTo" runat="server" Width="180px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="Label2" runat="server" Text="Make" ></asp:Label></strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMake" runat="server" Width="180px" ></asp:TextBox>
                        </td>
                        <td style="width: 10px;">
                        </td>
                        <td></td>
                          <td>
                            <strong>
                                <asp:CheckBox ID="chbIs_Active" Text="  Is Active" runat="server" Checked="true" /></strong>
                        </td>
                       
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="Label5" runat="server" Text="Chassis No"></asp:Label></strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtChassisno" runat="server" Width="180px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td style="width: 10px;">
                        </td>
                      
                    </tr>
                    <tr>
                       <td>
                            <strong>
                                <asp:Label ID="Label3" runat="server" Text="Model" Visible="false"></asp:Label></strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtModel" runat="server" Width="180px" Visible="false"></asp:TextBox>
                        </td> 
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="Label4" runat="server" Text="Engine No" Visible="false"></asp:Label></strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEngine" runat="server" Width="180px" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" OnClick="btnSave_Click"
                                Width="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;">
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="pnl_grd" runat="server" Width="765px" Height="250px" BorderWidth="1px"
                                BorderColor="Silver" ScrollBars="Vertical">
                                <asp:GridView ID="grd_vehicle" runat="server" ForeColor="SteelBlue" CssClass="gridRow2"
                                    HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White" OnRowCommand="grd_vehicle_RowCommand">
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                        PreviousPageText="Previous"></PagerSettings>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="tblhead" Height="30px" />
                                    <Columns>
                                        <asp:BoundField DataField="VEHICLE_ID" HeaderText="VEHICLE_ID">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VEHICLE_NO" HeaderText="Vehicle No">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                Width="85px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MAKE" HeaderText="Make">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MODEL" HeaderText="Model">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ENGINE_NO" HeaderText="Engine No">
                                         <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CHASSIS_NO" HeaderText="Chassis No">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USER_NAME" HeaderText="Delivery Man">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                Width="130px"></ItemStyle>
                                        </asp:BoundField>
                                         <asp:BoundField DataField="OrderBooker" HeaderText="">
                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                         <asp:BoundField DataField="Driver" HeaderText="Driver">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                Width="130px"></ItemStyle>
                                        </asp:BoundField>
                                         <asp:BoundField DataField="Loader" HeaderText="Loader">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                Width="130px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IS_ACTIVE" HeaderText="Is Active">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                Width="50px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DISTRIBUTOR_ID" HeaderText="DISTRIBUTOR_ID">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ASSIGN_TO" HeaderText="ASSIGN_ID">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
