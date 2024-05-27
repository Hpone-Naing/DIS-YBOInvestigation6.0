using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YBOInvestigation.Migrations
{
    public partial class CreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_DriverPunishmentType",
                columns: table => new
                {
                    PunishmentTypePkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Punishment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_DriverPunishmentType", x => x.PunishmentTypePkid);
                });

            migrationBuilder.CreateTable(
                name: "TB_FuelType",
                columns: table => new
                {
                    FuelTypePkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuelTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FuelType", x => x.FuelTypePkid);
                });

            migrationBuilder.CreateTable(
                name: "TB_Manufacturer",
                columns: table => new
                {
                    ManufacturerPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Manufacturer", x => x.ManufacturerPkid);
                });

            migrationBuilder.CreateTable(
                name: "TB_UserTypes",
                columns: table => new
                {
                    UserTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_UserTypes", x => x.UserTypeID);
                });

            migrationBuilder.CreateTable(
                name: "TB_YBSCompany",
                columns: table => new
                {
                    YBSCompanyPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YBSCompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_YBSCompany", x => x.YBSCompanyPkid);
                });

            migrationBuilder.CreateTable(
                name: "TB_Users",
                columns: table => new
                {
                    UserPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Users", x => x.UserPkid);
                    table.ForeignKey(
                        name: "FK_TB_Users_TB_UserTypes_UserTypeID",
                        column: x => x.UserTypeID,
                        principalTable: "TB_UserTypes",
                        principalColumn: "UserTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_YBSType",
                columns: table => new
                {
                    YBSTypePkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YBSTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YBSCompanyPkid = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_YBSType", x => x.YBSTypePkid);
                    table.ForeignKey(
                        name: "FK_TB_YBSType_TB_YBSCompany_YBSCompanyPkid",
                        column: x => x.YBSCompanyPkid,
                        principalTable: "TB_YBSCompany",
                        principalColumn: "YBSCompanyPkid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_VehicleData",
                columns: table => new
                {
                    VehicleDataPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    YBSName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VehicleNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ManufacturedYear = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CngQty = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CctvInstalled = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AssignedRoute = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TelematicDeviceInstalled = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TotalBusStop = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    POSInstalled = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OperatorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegistrantOperatorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    YBSCompanyPkid = table.Column<int>(type: "int", nullable: false),
                    VehicleTypePkid = table.Column<int>(type: "int", nullable: false),
                    FuelTypePkid = table.Column<int>(type: "int", nullable: false),
                    VehicleManufacturer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_VehicleData", x => x.VehicleDataPkid);
                    table.ForeignKey(
                        name: "FK_TB_VehicleData_TB_FuelType_FuelTypePkid",
                        column: x => x.FuelTypePkid,
                        principalTable: "TB_FuelType",
                        principalColumn: "FuelTypePkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_VehicleData_TB_Manufacturer_VehicleManufacturer",
                        column: x => x.VehicleManufacturer,
                        principalTable: "TB_Manufacturer",
                        principalColumn: "ManufacturerPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_VehicleData_TB_YBSCompany_YBSCompanyPkid",
                        column: x => x.YBSCompanyPkid,
                        principalTable: "TB_YBSCompany",
                        principalColumn: "YBSCompanyPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_VehicleData_TB_YBSType_VehicleTypePkid",
                        column: x => x.VehicleTypePkid,
                        principalTable: "TB_YBSType",
                        principalColumn: "YBSTypePkid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Driver",
                columns: table => new
                {
                    DriverPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DriverName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DriverLicense = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VehicleDataPkid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Driver", x => x.DriverPkid);
                    table.ForeignKey(
                        name: "FK_TB_Driver_TB_VehicleData_VehicleDataPkid",
                        column: x => x.VehicleDataPkid,
                        principalTable: "TB_VehicleData",
                        principalColumn: "VehicleDataPkid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_CallCenterInvestigationDept",
                columns: table => new
                {
                    CallCenterInvestigationDeptPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalRecord = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YbsRecordSender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RecordDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompletionStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YBSCompanyPkid = table.Column<int>(type: "int", nullable: false),
                    YBSTypePkid = table.Column<int>(type: "int", nullable: false),
                    DriverPkid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CallCenterInvestigationDept", x => x.CallCenterInvestigationDeptPkid);
                    table.ForeignKey(
                        name: "FK_TB_CallCenterInvestigationDept_TB_Driver_DriverPkid",
                        column: x => x.DriverPkid,
                        principalTable: "TB_Driver",
                        principalColumn: "DriverPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_CallCenterInvestigationDept_TB_YBSCompany_YBSCompanyPkid",
                        column: x => x.YBSCompanyPkid,
                        principalTable: "TB_YBSCompany",
                        principalColumn: "YBSCompanyPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_CallCenterInvestigationDept_TB_YBSType_YBSTypePkid",
                        column: x => x.YBSTypePkid,
                        principalTable: "TB_YBSType",
                        principalColumn: "YBSTypePkid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_InvestigationDept",
                columns: table => new
                {
                    YboRecordPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalRecord = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YbsRecordSender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RecordDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompletionStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChallanNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YBSCompanyPkid = table.Column<int>(type: "int", nullable: false),
                    YBSTypePkid = table.Column<int>(type: "int", nullable: false),
                    DriverPkid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_InvestigationDept", x => x.YboRecordPkid);
                    table.ForeignKey(
                        name: "FK_TB_InvestigationDept_TB_Driver_DriverPkid",
                        column: x => x.DriverPkid,
                        principalTable: "TB_Driver",
                        principalColumn: "DriverPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_InvestigationDept_TB_YBSCompany_YBSCompanyPkid",
                        column: x => x.YBSCompanyPkid,
                        principalTable: "TB_YBSCompany",
                        principalColumn: "YBSCompanyPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_InvestigationDept_TB_YBSType_YBSTypePkid",
                        column: x => x.YBSTypePkid,
                        principalTable: "TB_YBSType",
                        principalColumn: "YBSTypePkid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_SpecialEventInvestigationDept",
                columns: table => new
                {
                    SpecialEventInvestigationDeptDeptPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncidenceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IncidenceTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IncidencePlace = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VehicleNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReportTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    YbsRecordSender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YbsRecordSenderPosition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RecordDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Review = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActionResponse = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YBSCompanyPkid = table.Column<int>(type: "int", nullable: false),
                    YBSTypePkid = table.Column<int>(type: "int", nullable: false),
                    DriverPkid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SpecialEventInvestigationDept", x => x.SpecialEventInvestigationDeptDeptPkid);
                    table.ForeignKey(
                        name: "FK_TB_SpecialEventInvestigationDept_TB_Driver_DriverPkid",
                        column: x => x.DriverPkid,
                        principalTable: "TB_Driver",
                        principalColumn: "DriverPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_SpecialEventInvestigationDept_TB_YBSCompany_YBSCompanyPkid",
                        column: x => x.YBSCompanyPkid,
                        principalTable: "TB_YBSCompany",
                        principalColumn: "YBSCompanyPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_SpecialEventInvestigationDept_TB_YBSType_YBSTypePkid",
                        column: x => x.YBSTypePkid,
                        principalTable: "TB_YBSType",
                        principalColumn: "YBSTypePkid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_TrafficControlCenterInvestigationDept",
                columns: table => new
                {
                    TrafficControlCenterInvestigationDeptPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalRecord = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YbsRecordSender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RedLightCrossingPlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompletionStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YBSCompanyPkid = table.Column<int>(type: "int", nullable: false),
                    YBSTypePkid = table.Column<int>(type: "int", nullable: false),
                    DriverPkid = table.Column<int>(type: "int", nullable: false),
                    PunishmentTypePkid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TrafficControlCenterInvestigationDept", x => x.TrafficControlCenterInvestigationDeptPkid);
                    table.ForeignKey(
                        name: "FK_TB_TrafficControlCenterInvestigationDept_TB_Driver_DriverPkid",
                        column: x => x.DriverPkid,
                        principalTable: "TB_Driver",
                        principalColumn: "DriverPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_TrafficControlCenterInvestigationDept_TB_DriverPunishmentType_PunishmentTypePkid",
                        column: x => x.PunishmentTypePkid,
                        principalTable: "TB_DriverPunishmentType",
                        principalColumn: "PunishmentTypePkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_TrafficControlCenterInvestigationDept_TB_YBSCompany_YBSCompanyPkid",
                        column: x => x.YBSCompanyPkid,
                        principalTable: "TB_YBSCompany",
                        principalColumn: "YBSCompanyPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_TrafficControlCenterInvestigationDept_TB_YBSType_YBSTypePkid",
                        column: x => x.YBSTypePkid,
                        principalTable: "TB_YBSType",
                        principalColumn: "YBSTypePkid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_TrainedYBSDriverInfo",
                columns: table => new
                {
                    TrainedYBSDriverInfoPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EducationLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DriverPkid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TrainedYBSDriverInfo", x => x.TrainedYBSDriverInfoPkid);
                    table.ForeignKey(
                        name: "FK_TB_TrainedYBSDriverInfo_TB_Driver_DriverPkid",
                        column: x => x.DriverPkid,
                        principalTable: "TB_Driver",
                        principalColumn: "DriverPkid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_YBOInvestigationDept",
                columns: table => new
                {
                    YBOInvestigationDeptPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalRecord = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YbsRecordSender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompletionStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YBSCompanyPkid = table.Column<int>(type: "int", nullable: false),
                    YBSTypePkid = table.Column<int>(type: "int", nullable: false),
                    DriverPkid = table.Column<int>(type: "int", nullable: false),
                    PunishmentTypePkid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_YBOInvestigationDept", x => x.YBOInvestigationDeptPkid);
                    table.ForeignKey(
                        name: "FK_TB_YBOInvestigationDept_TB_Driver_DriverPkid",
                        column: x => x.DriverPkid,
                        principalTable: "TB_Driver",
                        principalColumn: "DriverPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_YBOInvestigationDept_TB_DriverPunishmentType_PunishmentTypePkid",
                        column: x => x.PunishmentTypePkid,
                        principalTable: "TB_DriverPunishmentType",
                        principalColumn: "PunishmentTypePkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_YBOInvestigationDept_TB_YBSCompany_YBSCompanyPkid",
                        column: x => x.YBSCompanyPkid,
                        principalTable: "TB_YBSCompany",
                        principalColumn: "YBSCompanyPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_YBOInvestigationDept_TB_YBSType_YBSTypePkid",
                        column: x => x.YBSTypePkid,
                        principalTable: "TB_YBSType",
                        principalColumn: "YBSTypePkid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_YBSDriverCourseDelivery",
                columns: table => new
                {
                    YBSDriverCourseDeliveryPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalRecord = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YBSCompanyPkid = table.Column<int>(type: "int", nullable: false),
                    YBSTypePkid = table.Column<int>(type: "int", nullable: false),
                    PunishmentTypePkid = table.Column<int>(type: "int", nullable: false),
                    TrainedYBSDriverInfoPkid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_YBSDriverCourseDelivery", x => x.YBSDriverCourseDeliveryPkid);
                    table.ForeignKey(
                        name: "FK_TB_YBSDriverCourseDelivery_TB_DriverPunishmentType_PunishmentTypePkid",
                        column: x => x.PunishmentTypePkid,
                        principalTable: "TB_DriverPunishmentType",
                        principalColumn: "PunishmentTypePkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_YBSDriverCourseDelivery_TB_TrainedYBSDriverInfo_TrainedYBSDriverInfoPkid",
                        column: x => x.TrainedYBSDriverInfoPkid,
                        principalTable: "TB_TrainedYBSDriverInfo",
                        principalColumn: "TrainedYBSDriverInfoPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_YBSDriverCourseDelivery_TB_YBSCompany_YBSCompanyPkid",
                        column: x => x.YBSCompanyPkid,
                        principalTable: "TB_YBSCompany",
                        principalColumn: "YBSCompanyPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_YBSDriverCourseDelivery_TB_YBSType_YBSTypePkid",
                        column: x => x.YBSTypePkid,
                        principalTable: "TB_YBSType",
                        principalColumn: "YBSTypePkid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_CallCenterInvestigationDept_DriverPkid",
                table: "TB_CallCenterInvestigationDept",
                column: "DriverPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_CallCenterInvestigationDept_YBSCompanyPkid",
                table: "TB_CallCenterInvestigationDept",
                column: "YBSCompanyPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_CallCenterInvestigationDept_YBSTypePkid",
                table: "TB_CallCenterInvestigationDept",
                column: "YBSTypePkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Driver_VehicleDataPkid",
                table: "TB_Driver",
                column: "VehicleDataPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_InvestigationDept_DriverPkid",
                table: "TB_InvestigationDept",
                column: "DriverPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_InvestigationDept_YBSCompanyPkid",
                table: "TB_InvestigationDept",
                column: "YBSCompanyPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_InvestigationDept_YBSTypePkid",
                table: "TB_InvestigationDept",
                column: "YBSTypePkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SpecialEventInvestigationDept_DriverPkid",
                table: "TB_SpecialEventInvestigationDept",
                column: "DriverPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SpecialEventInvestigationDept_YBSCompanyPkid",
                table: "TB_SpecialEventInvestigationDept",
                column: "YBSCompanyPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SpecialEventInvestigationDept_YBSTypePkid",
                table: "TB_SpecialEventInvestigationDept",
                column: "YBSTypePkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TrafficControlCenterInvestigationDept_DriverPkid",
                table: "TB_TrafficControlCenterInvestigationDept",
                column: "DriverPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TrafficControlCenterInvestigationDept_PunishmentTypePkid",
                table: "TB_TrafficControlCenterInvestigationDept",
                column: "PunishmentTypePkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TrafficControlCenterInvestigationDept_YBSCompanyPkid",
                table: "TB_TrafficControlCenterInvestigationDept",
                column: "YBSCompanyPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TrafficControlCenterInvestigationDept_YBSTypePkid",
                table: "TB_TrafficControlCenterInvestigationDept",
                column: "YBSTypePkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TrainedYBSDriverInfo_DriverPkid",
                table: "TB_TrainedYBSDriverInfo",
                column: "DriverPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Users_UserTypeID",
                table: "TB_Users",
                column: "UserTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_VehicleData_FuelTypePkid",
                table: "TB_VehicleData",
                column: "FuelTypePkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_VehicleData_VehicleManufacturer",
                table: "TB_VehicleData",
                column: "VehicleManufacturer");

            migrationBuilder.CreateIndex(
                name: "IX_TB_VehicleData_VehicleTypePkid",
                table: "TB_VehicleData",
                column: "VehicleTypePkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_VehicleData_YBSCompanyPkid",
                table: "TB_VehicleData",
                column: "YBSCompanyPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_YBOInvestigationDept_DriverPkid",
                table: "TB_YBOInvestigationDept",
                column: "DriverPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_YBOInvestigationDept_PunishmentTypePkid",
                table: "TB_YBOInvestigationDept",
                column: "PunishmentTypePkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_YBOInvestigationDept_YBSCompanyPkid",
                table: "TB_YBOInvestigationDept",
                column: "YBSCompanyPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_YBOInvestigationDept_YBSTypePkid",
                table: "TB_YBOInvestigationDept",
                column: "YBSTypePkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_YBSDriverCourseDelivery_PunishmentTypePkid",
                table: "TB_YBSDriverCourseDelivery",
                column: "PunishmentTypePkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_YBSDriverCourseDelivery_TrainedYBSDriverInfoPkid",
                table: "TB_YBSDriverCourseDelivery",
                column: "TrainedYBSDriverInfoPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_YBSDriverCourseDelivery_YBSCompanyPkid",
                table: "TB_YBSDriverCourseDelivery",
                column: "YBSCompanyPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_YBSDriverCourseDelivery_YBSTypePkid",
                table: "TB_YBSDriverCourseDelivery",
                column: "YBSTypePkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_YBSType_YBSCompanyPkid",
                table: "TB_YBSType",
                column: "YBSCompanyPkid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CallCenterInvestigationDept");

            migrationBuilder.DropTable(
                name: "TB_InvestigationDept");

            migrationBuilder.DropTable(
                name: "TB_SpecialEventInvestigationDept");

            migrationBuilder.DropTable(
                name: "TB_TrafficControlCenterInvestigationDept");

            migrationBuilder.DropTable(
                name: "TB_Users");

            migrationBuilder.DropTable(
                name: "TB_YBOInvestigationDept");

            migrationBuilder.DropTable(
                name: "TB_YBSDriverCourseDelivery");

            migrationBuilder.DropTable(
                name: "TB_UserTypes");

            migrationBuilder.DropTable(
                name: "TB_DriverPunishmentType");

            migrationBuilder.DropTable(
                name: "TB_TrainedYBSDriverInfo");

            migrationBuilder.DropTable(
                name: "TB_Driver");

            migrationBuilder.DropTable(
                name: "TB_VehicleData");

            migrationBuilder.DropTable(
                name: "TB_FuelType");

            migrationBuilder.DropTable(
                name: "TB_Manufacturer");

            migrationBuilder.DropTable(
                name: "TB_YBSType");

            migrationBuilder.DropTable(
                name: "TB_YBSCompany");
        }
    }
}
