using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TddDemo.Model;

namespace TddDemo.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20161004090213_Instructor")]
    partial class Instructor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TddDemo.Model.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnName("CourseID");

                    b.Property<int>("Credits");

                    b.Property<int>("DepartmentId")
                        .HasColumnName("DepartmentID");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("CourseId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Course");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Course");
                });

            modelBuilder.Entity("TddDemo.Model.CourseInstructor", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnName("CourseID");

                    b.Property<int>("PersonId")
                        .HasColumnName("PersonID");

                    b.HasKey("CourseId", "PersonId")
                        .HasName("PK_CourseInstructor");

                    b.HasIndex("CourseId");

                    b.HasIndex("PersonId");

                    b.ToTable("CourseInstructor");
                });

            modelBuilder.Entity("TddDemo.Model.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .HasColumnName("DepartmentID");

                    b.Property<int?>("Administrator");

                    b.Property<decimal>("Budget")
                        .HasColumnType("money");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("DepartmentId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("TddDemo.Model.OfficeAssignment", b =>
                {
                    b.Property<int>("InstructorId")
                        .HasColumnName("InstructorID");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<byte[]>("Timestamp")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp");

                    b.HasKey("InstructorId")
                        .HasName("PK_OfficeAssignment");

                    b.HasIndex("InstructorId")
                        .IsUnique();

                    b.ToTable("OfficeAssignment");
                });

            modelBuilder.Entity("TddDemo.Model.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PersonID");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime?>("EnrollmentDate")
                        .HasColumnType("datetime");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("HireDate")
                        .HasColumnType("datetime");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("PersonId");

                    b.ToTable("Person");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("TddDemo.Model.StudentGrade", b =>
                {
                    b.Property<int>("EnrollmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EnrollmentID");

                    b.Property<int>("CourseId")
                        .HasColumnName("CourseID");

                    b.Property<decimal?>("Grade")
                        .HasColumnType("decimal");

                    b.Property<int>("StudentId")
                        .HasColumnName("StudentID");

                    b.HasKey("EnrollmentId")
                        .HasName("PK_StudentGrade");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentGrade");
                });

            modelBuilder.Entity("TddDemo.Model.OnlineCourse", b =>
                {
                    b.HasBaseType("TddDemo.Model.Course");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnName("URL")
                        .HasAnnotation("MaxLength", 100);

                    b.ToTable("OnlineCourse");

                    b.HasDiscriminator().HasValue("OnlineCourse");
                });

            modelBuilder.Entity("TddDemo.Model.OnsiteCourse", b =>
                {
                    b.HasBaseType("TddDemo.Model.Course");

                    b.Property<string>("Days")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("Time")
                        .HasColumnType("smalldatetime");

                    b.ToTable("OnsiteCourse");

                    b.HasDiscriminator().HasValue("OnsiteCourse");
                });

            modelBuilder.Entity("TddDemo.Model.Instructor", b =>
                {
                    b.HasBaseType("TddDemo.Model.Person");


                    b.ToTable("Instructor");

                    b.HasDiscriminator().HasValue("Instructor");
                });

            modelBuilder.Entity("TddDemo.Model.Course", b =>
                {
                    b.HasOne("TddDemo.Model.Department", "Department")
                        .WithMany("Course")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("FK_Course_Department");
                });

            modelBuilder.Entity("TddDemo.Model.CourseInstructor", b =>
                {
                    b.HasOne("TddDemo.Model.Course", "Course")
                        .WithMany("CourseInstructor")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK_CourseInstructor_Course");

                    b.HasOne("TddDemo.Model.Person", "Person")
                        .WithMany("CourseInstructor")
                        .HasForeignKey("PersonId")
                        .HasConstraintName("FK_CourseInstructor_Person");
                });

            modelBuilder.Entity("TddDemo.Model.OfficeAssignment", b =>
                {
                    b.HasOne("TddDemo.Model.Person", "Instructor")
                        .WithOne("OfficeAssignment")
                        .HasForeignKey("TddDemo.Model.OfficeAssignment", "InstructorId")
                        .HasConstraintName("FK_OfficeAssignment_Person");
                });

            modelBuilder.Entity("TddDemo.Model.StudentGrade", b =>
                {
                    b.HasOne("TddDemo.Model.Course", "Course")
                        .WithMany("StudentGrade")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK_StudentGrade_Course");

                    b.HasOne("TddDemo.Model.Person", "Student")
                        .WithMany("StudentGrade")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_StudentGrade_Student");
                });
        }
    }
}
