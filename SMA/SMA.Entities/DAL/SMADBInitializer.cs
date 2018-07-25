using SMA.Entities.Models;
using SMA.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;


namespace SMA.Entities.Models
{
    //public class SMADBInitializer : DropCreateDatabaseIfModelChanges<SMAContext>
    //{
    //DropCreateDatabaseAlways
    //CreateDatabaseIfNotExists

    public class SMADBInitializer : DropCreateDatabaseIfModelChanges<SMAContext>
    {
       
        //DateTime date;
        protected override void Seed(SMAContext context)
        {
            var Contacts = new List<Contact>
            {
            new Contact{Name="Contact1",Address="Contact1 Address",PhoneNumber="100 000 0000", EmailAddress="Contact1@email.com", Website="www.Contact1.com", Note="Contact1 Note", ImagePath="~/Avator.png"},
            new Contact{Name="Contact2",Address="Contact2 Address",PhoneNumber="200 000 0000", EmailAddress="Contact2@email.com", Website="www.Contact2.com", Note="Contact2 Note", ImagePath="~/Avator.png"},
            new Contact{Name="Contact3",Address="Contact3 Address",PhoneNumber="300 000 0000", EmailAddress="Contact3@email.com", Website="www.Contact3.com", Note="Contact3 Note", ImagePath="~/Avator.png"},
            new Contact{Name="Contact4",Address="Contact4 Address",PhoneNumber="400 000 0000", EmailAddress="Contact4@email.com", Website="www.Contact4.com", Note="Contact4 Note", ImagePath="~/Avator.png"},
            new Contact{Name="Contact5",Address="Contact5 Address",PhoneNumber="500 000 0000", EmailAddress="Contact5@email.com", Website="www.Contact5.com", Note="Contact5 Note", ImagePath="~/Avator.png"},
            new Contact{Name="Contact6",Address="Contact6 Address",PhoneNumber="600 000 0000", EmailAddress="Contact6@email.com", Website="www.Contact6.com", Note="Contact6 Note", ImagePath="~/Avator.png"},
            
            };

            Contacts.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Contacts.Add(s);
                context.SaveChanges();

            });

            var Levels = new List<Level>
            {
            new Level{Libel="Level 1"},
            new Level{Libel="Level 2"},
            new Level{Libel="Level 3"},
            
            };

            Levels.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Levels.Add(s);
                context.SaveChanges();

            });

            var Images = new List<Image>
            {
            new Image{Alt="Image 1", Url="URL Image 1"},
            new Image{Alt="Image 2", Url="URL Image 2"},
            new Image{Alt="Image 3", Url="URL Image 3"},
            new Image{Alt="Image 4", Url="URL Image 4"},
            new Image{Alt="Image 5", Url="URL Image 5"},
            new Image{Alt="Image 6", Url="URL Image 6"},
            new Image{Alt="Image 7", Url="URL Image 7"},
            new Image{Alt="Image 8", Url="URL Image 8"},
            };

            Images.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Images.Add(s);
                context.SaveChanges();

            });

            var Classes = new List<Class>
            {
            new Class{Libel="Class 1", LevelId=context.Levels.Where(l=>l.Libel=="Level 1").FirstOrDefault().Id},
            new Class{Libel="Class 2", LevelId=context.Levels.Where(l=>l.Libel=="Level 2").FirstOrDefault().Id},
            new Class{Libel="Class 3", LevelId=context.Levels.Where(l=>l.Libel=="Level 3").FirstOrDefault().Id},

            //new Class{Libel="Class 1"},
            //new Class{Libel="Class 2"},
            //new Class{Libel="Class 3"},
            
            };

            Classes.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Classes.Add(s);
                context.SaveChanges();

            });

            var Students = new List<Student>
            {
            new Student{FirstName="Student ", LastName="1", Email="student1@yahoo.com" ,DropOut=false, Live=false, Genre="Homme",LieuNaissance="Sousse",dateBirth=DateTime.Today,tel="73 586 894",portable="20 645 789",adresse="rue sfax",ClassId=context.Classes.Where(c=>c.Libel=="Class 1").FirstOrDefault().Id, ImageId=context.Images.Where(i=>i.Alt=="Image 1").FirstOrDefault().Id},
            new Student{FirstName="Student ", LastName="2", Email="student2@yahoo.com" ,DropOut=false, Live=false, Genre="Homme",LieuNaissance="Sousse",dateBirth=DateTime.Today,tel="73 586 894",portable="20 645 789",adresse="rue sfax",ClassId=context.Classes.Where(c=>c.Libel=="Class 1").FirstOrDefault().Id, ImageId=context.Images.Where(i=>i.Alt=="Image 2").FirstOrDefault().Id},
            new Student{FirstName="Student ", LastName="3", Email="student3@yahoo.com" ,DropOut=false, Live=false, Genre="Homme",LieuNaissance="Sousse",dateBirth=DateTime.Today,tel="73 586 894",portable="20 645 789",adresse="rue sfax",ClassId=context.Classes.Where(c=>c.Libel=="Class 1").FirstOrDefault().Id, ImageId=context.Images.Where(i=>i.Alt=="Image 3").FirstOrDefault().Id},
            new Student{FirstName="Student ", LastName="4", Email="student4@yahoo.com" ,DropOut=false, Live=false, Genre="Homme",LieuNaissance="Sousse",dateBirth=DateTime.Today,tel="73 586 894",portable="20 645 789",adresse="rue sfax",ClassId=context.Classes.Where(c=>c.Libel=="Class 1").FirstOrDefault().Id, ImageId=context.Images.Where(i=>i.Alt=="Image 4").FirstOrDefault().Id},
            new Student{FirstName="Student ", LastName="5", Email="student5@yahoo.com" ,DropOut=false, Live=false, Genre="Homme",LieuNaissance="Sousse",dateBirth=DateTime.Today,tel="73 586 894",portable="20 645 789",adresse="rue sfax",ClassId=context.Classes.Where(c=>c.Libel=="Class 2").FirstOrDefault().Id, ImageId=context.Images.Where(i=>i.Alt=="Image 5").FirstOrDefault().Id},
            new Student{FirstName="Student ", LastName="6", Email="student6@yahoo.com" ,DropOut=false, Live=false, Genre="Homme",LieuNaissance="Sousse",dateBirth=DateTime.Today,tel="73 586 894",portable="20 645 789",adresse="rue sfax",ClassId=context.Classes.Where(c=>c.Libel=="Class 2").FirstOrDefault().Id, ImageId=context.Images.Where(i=>i.Alt=="Image 6").FirstOrDefault().Id},
            new Student{FirstName="Student ", LastName="7", Email="student7@yahoo.com" ,DropOut=false, Live=false, Genre="Homme",LieuNaissance="Sousse",dateBirth=DateTime.Today,tel="73 586 894",portable="20 645 789",adresse="rue sfax",ClassId=context.Classes.Where(c=>c.Libel=="Class 2").FirstOrDefault().Id, ImageId=context.Images.Where(i=>i.Alt=="Image 7").FirstOrDefault().Id},
            new Student{FirstName="Student ", LastName="8", Email="student8@yahoo.com" ,DropOut=false, Live=false, Genre="Homme",LieuNaissance="Sousse",dateBirth=DateTime.Today,tel="73 586 894",portable="20 645 789",adresse="rue sfax",ClassId=context.Classes.Where(c=>c.Libel=="Class 3").FirstOrDefault().Id, ImageId=context.Images.Where(i=>i.Alt=="Image 8").FirstOrDefault().Id},
            new Student{FirstName="sam", LastName="9", Email="student9@yahoo.com" ,DropOut=false, Live=false, Genre="Femme",LieuNaissance="Sousse",dateBirth=DateTime.Today,tel="73 586 894",portable="20 645 789",adresse="rue sfax",ClassId=context.Classes.Where(c=>c.Libel=="Class 3").FirstOrDefault().Id, ImageId=context.Images.Where(i=>i.Alt=="Image 8").FirstOrDefault().Id},
            

            //new Student{FirstName="Student ", LastName="1", DropOut=false, Live=false, ClassId=context.Classes.Where(c=>c.Libel=="Class 1").FirstOrDefault().Id},
            //new Student{FirstName="Student ", LastName="2", DropOut=false, Live=false, ClassId=context.Classes.Where(c=>c.Libel=="Class 1").FirstOrDefault().Id},
            //new Student{FirstName="Student ", LastName="3", DropOut=false, Live=false, ClassId=context.Classes.Where(c=>c.Libel=="Class 1").FirstOrDefault().Id},
            //new Student{FirstName="Student ", LastName="4", DropOut=false, Live=false, ClassId=context.Classes.Where(c=>c.Libel=="Class 1").FirstOrDefault().Id},
            //new Student{FirstName="Student ", LastName="5", DropOut=false, Live=false, ClassId=context.Classes.Where(c=>c.Libel=="Class 2").FirstOrDefault().Id},
            //new Student{FirstName="Student ", LastName="6", DropOut=false, Live=false, ClassId=context.Classes.Where(c=>c.Libel=="Class 2").FirstOrDefault().Id},
            //new Student{FirstName="Student ", LastName="7", DropOut=false, Live=false, ClassId=context.Classes.Where(c=>c.Libel=="Class 2").FirstOrDefault().Id},
            //new Student{FirstName="Student ", LastName="8", DropOut=false, Live=false, ClassId=context.Classes.Where(c=>c.Libel=="Class 3").FirstOrDefault().Id},
            


            };

            Students.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Students.Add(s);
                context.SaveChanges();

            });




            var Terms = new List<Term>
            {
            new Term{Libel="Term1", Begin=DateTime.Today, End=DateTime.Today},
            new Term{Libel="Term2", Begin=DateTime.Today, End=DateTime.Today},
            new Term{Libel="Term3", Begin=DateTime.Today, End=DateTime.Today},
            new Term{Libel="Term4", Begin=DateTime.Today, End=DateTime.Today},

            };

            Terms.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Terms.Add(s);
                context.SaveChanges();

            });

            var Assessments = new List<Assessment>
            {
            new Assessment{Libel="Assessment1", Coef= 1, TermId=context.Terms.Where(t=>t.Libel=="Term1").FirstOrDefault().Id},
            new Assessment{Libel="Assessment2", Coef= 2, TermId=context.Terms.Where(t=>t.Libel=="Term2").FirstOrDefault().Id},
            new Assessment{Libel="Assessment3", Coef= 3, TermId=context.Terms.Where(t=>t.Libel=="Term3").FirstOrDefault().Id},
            new Assessment{Libel="Assessment4", Coef= 4, TermId=context.Terms.Where(t=>t.Libel=="Term4").FirstOrDefault().Id},

            };

            Assessments.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Assessments.Add(s);
                context.SaveChanges();

            });



            var SchoolYears = new List<SchoolYear>
            {

            new SchoolYear{Begin=DateTime.Today,End=DateTime.Today},
            

            };

            SchoolYears.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.SchoolYears.Add(s);
                context.SaveChanges();

            });


            var Subjects = new List<Subject>
            {
            new Subject{Libel="Anglais"},
            new Subject{Libel="Mathématique"},
            new Subject{Libel="Français"},
            new Subject{Libel="Sciences Naturelles"},

            };

            Subjects.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Subjects.Add(s);
                context.SaveChanges();

            });

            var Groups = new List<Group>
            {
            new Group{ Name="Teachers"},
            new Group{ Name="Parents"},
            new Group{ Name="SchoolHeads"},
            };

            Groups.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Groups.Add(s);
                context.SaveChanges();

            });


            var Teachers = new List<Teacher>
            {
            new Teacher{Login="login6", Password=Hashing.Encrypt("Password6",true), GroupId=context.Groups.Where(g=>g.Name=="Teachers").FirstOrDefault().Id, FirstName="mohamed",LastName="Teacher1", Leave=true, ImageId=context.Images.Where(i=>i.Alt=="Image 1").FirstOrDefault().Id},
            new Teacher{Login="login7", Password=Hashing.Encrypt("Password7",true), GroupId=context.Groups.Where(g=>g.Name=="Teachers").FirstOrDefault().Id, FirstName="mouna",LastName="Teacher2", Leave=true, ImageId=context.Images.Where(i=>i.Alt=="Image 2").FirstOrDefault().Id},
            new Teacher{Login="login8", Password=Hashing.Encrypt("Password8",true), GroupId=context.Groups.Where(g=>g.Name=="Teachers").FirstOrDefault().Id, FirstName="haythem",LastName="Teacher3", Leave=true, ImageId=context.Images.Where(i=>i.Alt=="Image 3").FirstOrDefault().Id},
            new Teacher{Login="login9", Password=Hashing.Encrypt("Password9",true), GroupId=context.Groups.Where(g=>g.Name=="Teachers").FirstOrDefault().Id, FirstName="hela",LastName="Teacher4", Leave=true, ImageId=context.Images.Where(i=>i.Alt=="Image 4").FirstOrDefault().Id},

            };

            Teachers.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Teachers.Add(s);
                context.SaveChanges();

            });


            var ClassRooms = new List<ClassRoom>
            {
            new ClassRoom{Libel="ClassRoom1"},
            new ClassRoom{Libel="ClassRoom2"},
            new ClassRoom{Libel="ClassRoom3"},
            new ClassRoom{Libel="ClassRoom4"},

            };

            ClassRooms.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.ClassRooms.Add(s);
                context.SaveChanges();

            });



            var SubjectLevels = new List<SubjectLevel>
            {
            new SubjectLevel{Coef=1, SubjectId=context.Subjects.Where(cb=>cb.Libel=="Anglais").FirstOrDefault().Id, LevelId=context.Levels.Where(l=>l.Libel=="Level 1").FirstOrDefault().Id},
            new SubjectLevel{Coef=2, SubjectId=context.Subjects.Where(cb=>cb.Libel=="Mathématique").FirstOrDefault().Id, LevelId=context.Levels.Where(l=>l.Libel=="Level 1").FirstOrDefault().Id},
            new SubjectLevel{Coef=3, SubjectId=context.Subjects.Where(cb=>cb.Libel=="Français").FirstOrDefault().Id, LevelId=context.Levels.Where(l=>l.Libel=="Level 2").FirstOrDefault().Id},
            new SubjectLevel{Coef=4, SubjectId=context.Subjects.Where(cb=>cb.Libel=="Sciences Naturelles").FirstOrDefault().Id, LevelId=context.Levels.Where(l=>l.Libel=="Level 2").FirstOrDefault().Id},

            };

            SubjectLevels.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.SubjectLevels.Add(s);
                context.SaveChanges();

            });




            var beginDateTime = DateTime.Today;
            var endDateTime = DateTime.Today;
            var classme = context.Classes.Where(c => c.Libel == "Class 1").First().Id;
            var sublevelme = context.SubjectLevels.Where(cbv => cbv.Coef == 1).First().Id;
            var classroomme = context.ClassRooms.Where(cr => cr.Libel == "ClassRoom1").First().Id;
            var teacherme = context.Teachers.Where(t => t.FirstName == "mouna").First().Id;
            var schoolYearBeginDate = DateTime.Today;
            var schoolyearme = context.SchoolYears.Where(f => f.Begin == schoolYearBeginDate).First().Id;

            var Schedules = new List<Schedule>
{
    new Schedule
    { 
        Day="Lundi", 
        Note="note", 
        Begin = beginDateTime, 
        End=endDateTime, 
        ClassId = classme, 
        SubjectLevelId = sublevelme, 
        ClassRoomId = classroomme, 
        TeacherId = teacherme, 
        SchoolYearId= schoolyearme,
    },
    new Schedule
    { 
        Day="Mardi", 
        Note="note", 
        Begin = beginDateTime, 
        End=endDateTime, 
        ClassId = classme, 
        SubjectLevelId = sublevelme, 
        ClassRoomId = classroomme, 
        TeacherId = teacherme, 
        SchoolYearId= schoolyearme,
    },
     new Schedule
    { 
        Day="Mercredi", 
        Note="note", 
        Begin = beginDateTime, 
        End=endDateTime, 
        ClassId = classme, 
        SubjectLevelId = sublevelme, 
        ClassRoomId = classroomme, 
        TeacherId = teacherme, 
        SchoolYearId= schoolyearme,
    },
};


            Schedules.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Schedules.Add(s);
                context.SaveChanges();

            });



            var schoolyeac = context.SchoolYears.Where(f => f.Begin == schoolYearBeginDate).First().Id;

            var Attendances = new List<Attendance>
            {
            new Attendance{IsPresent=true , CreatedDate=DateTime.Today, StudentId=context.Students.Where(c=>c.FirstName=="Student").FirstOrDefault().Id, ScheduleId=context.Schedules.Where(cc=>cc.Day=="Lundi").FirstOrDefault().Id, SchoolYearId=schoolyeac},
            new Attendance{IsPresent=false , CreatedDate=DateTime.Today, StudentId=context.Students.Where(c=>c.FirstName=="sam").FirstOrDefault().Id, ScheduleId=context.Schedules.Where(cc=>cc.Day=="Mardi").FirstOrDefault().Id, SchoolYearId=schoolyeac},
            //new Attendance{IsPresent=true , CreatedDate=DateTime.Today, StudentId=context.Students.Where(c=>c.FirstName=="Student").FirstOrDefault().Id, ScheduleId=context.Schedules.Where(cc=>cc.Day=="Mardi").FirstOrDefault().Id, SchoolYearId=context.SchoolYears.Where(cx=>cx.Begin==DateTime.Today).FirstOrDefault().Id},
            //new Attendance{IsPresent=true , CreatedDate=DateTime.Today, StudentId=context.Students.Where(c=>c.FirstName=="Student").FirstOrDefault().Id, ScheduleId=context.Schedules.Where(cc=>cc.Day=="Mercredi").FirstOrDefault().Id, SchoolYearId=context.SchoolYears.Where(cx=>cx.Begin==DateTime.Today).FirstOrDefault().Id},
            //new Attendance{IsPresent=true , CreatedDate=DateTime.Today, StudentId=context.Students.Where(c=>c.FirstName=="Student").FirstOrDefault().Id, ScheduleId=context.Schedules.Where(cc=>cc.Day=="Lundi").FirstOrDefault().Id, SchoolYearId=context.SchoolYears.Where(cx=>cx.Begin==DateTime.Today).FirstOrDefault().Id},
            //new Attendance{IsPresent=true , CreatedDate=DateTime.Today, StudentId=context.Students.Where(c=>c.FirstName=="Student").FirstOrDefault().Id, ScheduleId=context.Schedules.Where(cc=>cc.Day=="Mardi").FirstOrDefault().Id, SchoolYearId=context.SchoolYears.Where(cx=>cx.Begin==DateTime.Today).FirstOrDefault().Id},
           
            

            };

            Attendances.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Attendances.Add(s);
                context.SaveChanges();

            });


            var schoolyeacc = context.SchoolYears.Where(f => f.Begin == schoolYearBeginDate).First().Id;
            var Exams = new List<Exam>
            {
              new Exam{At=DateTime.Today , Begin=DateTime.Today, End=DateTime.Today, ClassId=context.Classes.Where(c=>c.Libel=="Class 1").FirstOrDefault().Id, SubjectId=context.Subjects.Where(cb=>cb.Libel=="Anglais").FirstOrDefault().Id, ClassRoomId=context.ClassRooms.Where(cr=>cr.Libel=="ClassRoom1").FirstOrDefault().Id, SchoolYearId=schoolyeacc, AssessmentId=context.Assessments.Where(aa=>aa.Libel=="Assessment1").FirstOrDefault().Id},
              new Exam{At=DateTime.Today , Begin=DateTime.Today, End=DateTime.Today, ClassId=context.Classes.Where(c=>c.Libel=="Class 2").FirstOrDefault().Id, SubjectId=context.Subjects.Where(cb=>cb.Libel=="Mathématique").FirstOrDefault().Id, ClassRoomId=context.ClassRooms.Where(cr=>cr.Libel=="ClassRoom2").FirstOrDefault().Id, SchoolYearId=schoolyeacc, AssessmentId=context.Assessments.Where(aa=>aa.Libel=="Assessment2").FirstOrDefault().Id},
              new Exam{At=DateTime.Today , Begin=DateTime.Today, End=DateTime.Today, ClassId=context.Classes.Where(c=>c.Libel=="Class 3").FirstOrDefault().Id, SubjectId=context.Subjects.Where(cb=>cb.Libel=="Français").FirstOrDefault().Id, ClassRoomId=context.ClassRooms.Where(cr=>cr.Libel=="ClassRoom3").FirstOrDefault().Id, SchoolYearId=schoolyeacc, AssessmentId=context.Assessments.Where(aa=>aa.Libel=="Assessment1").FirstOrDefault().Id},
              new Exam{At=DateTime.Today , Begin=DateTime.Today, End=DateTime.Today, ClassId=context.Classes.Where(c=>c.Libel=="Class 1").FirstOrDefault().Id, SubjectId=context.Subjects.Where(cb=>cb.Libel=="Sciences Naturelles").FirstOrDefault().Id, ClassRoomId=context.ClassRooms.Where(cr=>cr.Libel=="ClassRoom4").FirstOrDefault().Id, SchoolYearId=schoolyeacc, AssessmentId=context.Assessments.Where(aa=>aa.Libel=="Assessment2").FirstOrDefault().Id},
              new Exam{At=DateTime.Today , Begin=DateTime.Today, End=DateTime.Today, ClassId=context.Classes.Where(c=>c.Libel=="Class 2").FirstOrDefault().Id, SubjectId=context.Subjects.Where(cb=>cb.Libel=="Anglais").FirstOrDefault().Id, ClassRoomId=context.ClassRooms.Where(cr=>cr.Libel=="ClassRoom1").FirstOrDefault().Id, SchoolYearId=schoolyeacc, AssessmentId=context.Assessments.Where(aa=>aa.Libel=="Assessment1").FirstOrDefault().Id},
           
            

            };

            Exams.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Exams.Add(s);
                context.SaveChanges();

            });

            var submarks1 = context.Subjects.Where(cb => cb.Libel == "Anglais").FirstOrDefault().Id;
            var submarks2 = context.Subjects.Where(cb => cb.Libel == "Mathématique").FirstOrDefault().Id;
            var submarks3 = context.Subjects.Where(cb => cb.Libel == "Français").FirstOrDefault().Id;
            var submarks4 = context.Subjects.Where(cb => cb.Libel == "Sciences Naturelles").FirstOrDefault().Id;
            var studentmarks = context.Students.Where(c => c.FirstName == "Student").FirstOrDefault().Id;
            var studentmarks1 = context.Students.Where(c => c.FirstName == "sam").FirstOrDefault().Id;
            var schoolyeaccv = context.SchoolYears.Where(f => f.Begin == schoolYearBeginDate).First().Id;
            var ExamBeginDate = DateTime.Today;
            var exammarks = context.Exams.Where(aax => aax.At == ExamBeginDate).First().Id;
            var Marks = new List<Mark>
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            new Mark{Score=234 , Note="15", SubjectId=submarks1, StudentId=studentmarks, TeacherId=context.Teachers.Where(c=>c.FirstName=="mouna").FirstOrDefault().Id, SchoolYearId=schoolyeaccv, AssessmentId=context.Assessments.Where(aa=>aa.Libel=="Assessment1").FirstOrDefault().Id , ExamId=exammarks},
            new Mark{Score=235 , Note="16", SubjectId=submarks2, StudentId=studentmarks, TeacherId=context.Teachers.Where(c=>c.FirstName=="mohamed").FirstOrDefault().Id, SchoolYearId=schoolyeaccv, AssessmentId=context.Assessments.Where(aa=>aa.Libel=="Assessment2").FirstOrDefault().Id , ExamId=exammarks},
            new Mark{Score=236 , Note="8", SubjectId=submarks3, StudentId=studentmarks1, TeacherId=context.Teachers.Where(c=>c.FirstName=="haythem").FirstOrDefault().Id, SchoolYearId=schoolyeaccv, AssessmentId=context.Assessments.Where(aa=>aa.Libel=="Assessment3").FirstOrDefault().Id , ExamId=exammarks},
            new Mark{Score=237 , Note="2", SubjectId=submarks4, StudentId=studentmarks1, TeacherId=context.Teachers.Where(c=>c.FirstName=="hela").FirstOrDefault().Id, SchoolYearId=schoolyeaccv, AssessmentId=context.Assessments.Where(aa=>aa.Libel=="Assessment4").FirstOrDefault().Id , ExamId=exammarks},
            //new Mark{Score= 235 , Note="16", SubjectId=context.Subjects.Where(cb=>cb.Libel=="Anglais").FirstOrDefault().Id, StudentId=context.Students.Where(c=>c.FirstName=="Student").FirstOrDefault().Id, TeacherId=context.Teachers.Where(c=>c.FirstName=="Teacher2").FirstOrDefault().Id, SchoolYearId=context.SchoolYears.Where(c=>c.Begin==DateTime.Today).FirstOrDefault().Id, AssessmentId=context.Assessments.Where(aa=>aa.Libel=="Assessment1").FirstOrDefault().Id , ExamId=context.Exams.Where(aax=>aax.At==DateTime.Today).FirstOrDefault().Id},
            //new Mark{Score= 236 , Note="17", SubjectId=context.Subjects.Where(cb=>cb.Libel=="Anglais").FirstOrDefault().Id, StudentId=context.Students.Where(c=>c.FirstName=="Student").FirstOrDefault().Id, TeacherId=context.Teachers.Where(c=>c.FirstName=="Teacher2").FirstOrDefault().Id, SchoolYearId=context.SchoolYears.Where(c=>c.Begin==DateTime.Today).FirstOrDefault().Id, AssessmentId=context.Assessments.Where(aa=>aa.Libel=="Assessment1").FirstOrDefault().Id , ExamId=context.Exams.Where(aax=>aax.At==DateTime.Today).FirstOrDefault().Id},
            //new Mark{Score= 237 , Note="18", SubjectId=context.Subjects.Where(cb=>cb.Libel=="Anglais").FirstOrDefault().Id, StudentId=context.Students.Where(c=>c.FirstName=="Student").FirstOrDefault().Id, TeacherId=context.Teachers.Where(c=>c.FirstName=="Teacher2").FirstOrDefault().Id, SchoolYearId=context.SchoolYears.Where(c=>c.Begin==DateTime.Today).FirstOrDefault().Id, AssessmentId=context.Assessments.Where(aa=>aa.Libel=="Assessment1").FirstOrDefault().Id , ExamId=context.Exams.Where(aax=>aax.At==DateTime.Today).FirstOrDefault().Id},

            };

            Marks.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Marks.Add(s);
                context.SaveChanges();

            });





            var SchoolHeads = new List<SchoolHead>
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            new SchoolHead{Login="login10", Password=Hashing.Encrypt("Password10",true), GroupId=context.Groups.Where(g=>g.Name=="SchoolHeads").FirstOrDefault().Id, FirstName="ouni" , LastName="mahmoud", ImageId=context.Images.Where(i=>i.Alt=="Image 1").FirstOrDefault().Id},
            new SchoolHead{Login="login11", Password=Hashing.Encrypt("Password11",true), GroupId=context.Groups.Where(g=>g.Name=="SchoolHeads").FirstOrDefault().Id, FirstName="tawfik" , LastName="hamdi", ImageId=context.Images.Where(i=>i.Alt=="Image 2").FirstOrDefault().Id},
            new SchoolHead{Login="login12", Password=Hashing.Encrypt("Password12",true), GroupId=context.Groups.Where(g=>g.Name=="SchoolHeads").FirstOrDefault().Id, FirstName="salloum" , LastName="aycha", ImageId=context.Images.Where(i=>i.Alt=="Image 3").FirstOrDefault().Id},
            new SchoolHead{Login="login13", Password=Hashing.Encrypt("Password13",true), GroupId=context.Groups.Where(g=>g.Name=="SchoolHeads").FirstOrDefault().Id, FirstName="snoussi" , LastName="hamza", ImageId=context.Images.Where(i=>i.Alt=="Image 4").FirstOrDefault().Id},
            };

            SchoolHeads.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.SchoolHeads.Add(s);
                context.SaveChanges();

            });


            //var Users = new List<UserSMA>
            //{                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            //new UserSMA{},
            //};

            //Users.ForEach(s =>
            //{
            //    s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
            //    context.Users.Add(s);
            //    context.SaveChanges();

            //});

            var Parents = new List<Parent>
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            new Parent{Login="login1", Password=Hashing.Encrypt("Password1",true),GroupId=context.Groups.Where(g=>g.Name=="Parents").FirstOrDefault().Id, ImageId=context.Images.Where(i=>i.Alt=="Image 1").FirstOrDefault().Id, FirstName="ouni"    , LastName="mahmoud" , Email="ouni@yahoo.com" ,Gender="Homme" , Adress="rue ennasr 2 Sousse" ,Tel="73 416 899" ,Mobile="54 945 897" ,Profession="Directeur Finance" },
            new Parent{Login="login2", Password=Hashing.Encrypt("Password2",true),GroupId=context.Groups.Where(g=>g.Name=="Parents").FirstOrDefault().Id, ImageId=context.Images.Where(i=>i.Alt=="Image 2").FirstOrDefault().Id, FirstName="snoussi" , LastName="hamza",Email="hamza@yahoo.com" ,Gender="Homme" , Adress="rue ouardanine 3 Monastir" ,Tel="73 436 897" ,Mobile="20 456 468" ,Profession="Directeur STEG" },
            new Parent{Login="login3", Password=Hashing.Encrypt("Password3",true),GroupId=context.Groups.Where(g=>g.Name=="Parents").FirstOrDefault().Id, ImageId=context.Images.Where(i=>i.Alt=="Image 3").FirstOrDefault().Id, FirstName="salloum" , LastName="aycha",Email="salloum_aycha@yahoo.com" , Gender="Femme" , Adress="rue elghazelle 1 Sahloul" ,Tel="73 456 897" ,Mobile="92 966 843" ,Profession="Maitresse" },
            new Parent{Login="login4", Password=Hashing.Encrypt("Password4",true),GroupId=context.Groups.Where(g=>g.Name=="Parents").FirstOrDefault().Id, ImageId=context.Images.Where(i=>i.Alt=="Image 4").FirstOrDefault().Id, FirstName="tawfik"  , LastName="hamza",Email="hamza@yahoo.fr" , Gender="Homme" , Adress="rue ennasr 8 Sousse" ,Tel="73 656 957" ,Mobile="21 824 149" ,Profession="Forgeron" },
            };

            Parents.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Parents.Add(s);
                context.SaveChanges();

            });

            var test = context.Parents.Where(i => i.FirstName == "ouni").FirstOrDefault().Id;
            var test1 = context.Parents.Where(i => i.FirstName == "snoussi").FirstOrDefault().Id;
            var test2 = context.Parents.Where(i => i.FirstName == "salloum").FirstOrDefault().Id;
            var ccv = context.SchoolHeads.Where(s => s.FirstName == "ouni").First().Id;
            var ccv1 = context.SchoolHeads.Where(s => s.FirstName == "tawfik").First().Id;
            var ccv2 = context.SchoolHeads.Where(s => s.FirstName == "snoussi").First().Id;
            var Messages = new List<Message>
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            new Message{Content="message1" , CreatedDate=DateTime.Today, IsRed=true, ParentId=test, UserSMAId=ccv},
            new Message{Content="message2" , CreatedDate=DateTime.Today, IsRed=true, ParentId=test1, UserSMAId=ccv1},
            new Message{Content="message3" , CreatedDate=DateTime.Today, IsRed=false, ParentId=test2, UserSMAId=ccv2},
            new Message{Content="message4" , CreatedDate=DateTime.Today, IsRed=true, ParentId=test1, UserSMAId=ccv1},
            //new Message{Content="message2" , CreatedDate=DateTime.Today, IsRed=false, ParentId=context.Parents.Where(i=>i.FirstName=="snoussi").FirstOrDefault().Id, UserSMAId=context.SchoolHeads.Where(s=>s.FirstName =="ouni").First().Id},
            
            };

            Messages.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Messages.Add(s);
                context.SaveChanges();

            });


            var ParentHasStudents = new List<ParentHasStudent>
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            new ParentHasStudent{ParentId=context.Parents.Where(i=>i.FirstName=="ouni").FirstOrDefault().Id, StudentId=context.Students.Where(c=>c.FirstName=="Student").FirstOrDefault().Id},
            new ParentHasStudent{ParentId=context.Parents.Where(i=>i.FirstName=="snoussi").FirstOrDefault().Id, StudentId=context.Students.Where(c=>c.FirstName=="Student").FirstOrDefault().Id},
            new ParentHasStudent{ParentId=context.Parents.Where(i=>i.FirstName=="salloum").FirstOrDefault().Id, StudentId=context.Students.Where(c=>c.FirstName=="Student").FirstOrDefault().Id},
            new ParentHasStudent{ParentId=context.Parents.Where(i=>i.FirstName=="tawfik").FirstOrDefault().Id, StudentId=context.Students.Where(c=>c.FirstName=="Student").FirstOrDefault().Id},
            };

            ParentHasStudents.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.ParentHasStudents.Add(s);
                context.SaveChanges();

            });



            var PunishmentTypes = new List<PunishmentType>
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            new PunishmentType{Libel="punition1"},
            new PunishmentType{Libel="punition2"},
            new PunishmentType{Libel="punition3"},
            new PunishmentType{Libel="punition4"},
            };

            PunishmentTypes.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.PunishmentTypes.Add(s);
                context.SaveChanges();

            });


            var schoolyeactt = context.SchoolYears.Where(f => f.Begin == schoolYearBeginDate).First().Id;
            var ttv = context.PunishmentTypes.Where(c => c.Libel == "punition1").FirstOrDefault().Id;
            var Punishments = new List<Punishment>
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            new Punishment{Note="notepunition", TeacherId=context.Teachers.Where(t=>t.FirstName=="mouna").First().Id, StudentId=context.Students.Where(c=>c.FirstName=="Student").FirstOrDefault().Id, SchoolYearId=schoolyeactt, PunishmentTypeId=ttv},
            //new Punishment{Note="notepunition", TeacherId=context.Teachers.Where(t=>t.FirstName=="mouna").First().Id, StudentId=context.Students.Where(c=>c.FirstName=="Student").FirstOrDefault().Id, SchoolYearId=schoolyeactt, PunishmentTypeId=context.PunishmentTypes.Where(c=>c.Libel=="punition2").FirstOrDefault().Id},
            };

            Punishments.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Punishments.Add(s);
                context.SaveChanges();

            });


            var SSF = context.SchoolHeads.Where(c => c.FirstName == "ouni").FirstOrDefault().Id;
            var SSF1 = context.SchoolHeads.Where(c => c.FirstName == "tawfik").FirstOrDefault().Id;
            var Holidays = new List<Holiday>
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            new Holiday{Libel="Holiday1", Begin=DateTime.Today,End=DateTime.Today, SchoolHeadId=SSF},
            new Holiday{Libel="Holiday2", Begin=DateTime.Today,End=DateTime.Today, SchoolHeadId=SSF},
            new Holiday{Libel="Holiday3", Begin=DateTime.Today,End=DateTime.Today, SchoolHeadId=SSF1},
            //new Holiday{Libel="Holiday2", Begin=DateTime.Today,End=DateTime.Today, SchoolHeadId=context.SchoolHeads.Where(c=>c.FirstName=="tawfik").FirstOrDefault().Id},
            };

            Holidays.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Holidays.Add(s);
                context.SaveChanges();

            });



            var FeedTypes = new List<FeedType>
            {
            new FeedType{Libel="FeedType1"},
            new FeedType{Libel="FeedType2"},
            new FeedType{Libel="FeedType3"},

            };


            FeedTypes.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.FeedTypes.Add(s);
                context.SaveChanges();

            });



            var feedtypeee = context.FeedTypes.Where(s => s.Libel == "FeedType1").First().Id;
            var feedtypeee1 = context.FeedTypes.Where(s => s.Libel == "FeedType2").First().Id;
            var feedtypeee2 = context.FeedTypes.Where(s => s.Libel == "FeedType3").First().Id;
            var Events = new List<Event>
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            new Event{Begin=DateTime.Today,End=DateTime.Today, UserSMAId=ccv,Libel="Libel1", Content="contentnotif1" , CreatedDate=DateTime.Today, FeedTypeId=feedtypeee , ImageId=context.Images.Where(i=>i.Alt=="Image 1").FirstOrDefault().Id},
            new Event{Begin=DateTime.Today,End=DateTime.Today, UserSMAId=ccv,Libel="Libel2", Content="contentnotif2" , CreatedDate=DateTime.Today, FeedTypeId=feedtypeee1 , ImageId=context.Images.Where(i=>i.Alt=="Image 2").FirstOrDefault().Id},
            new Event{Begin=DateTime.Today,End=DateTime.Today, UserSMAId=ccv,Libel="Libel3", Content="contentnotif3" , CreatedDate=DateTime.Today, FeedTypeId=feedtypeee2 , ImageId=context.Images.Where(i=>i.Alt=="Image 3").FirstOrDefault().Id},
            //new Event{Begin=DateTime.Today,End=DateTime.Today, UserSMAId=context.SchoolHeads.Where(s=>s.FirstName =="ouni").FirstOrDefault().Id},
            };

            Events.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Events.Add(s);
                context.SaveChanges();

            });


            var FeedComments = new List<FeedComment>
            {
            new FeedComment{Content="Content1", CreatedDate=DateTime.Today, FeedId=context.Feeds.Where(s=>s.Libel=="Libel1").FirstOrDefault().Id, UserSMAId=ccv},
            new FeedComment{Content="Content2", CreatedDate=DateTime.Today, FeedId=context.Feeds.Where(s=>s.Libel=="Libel2").FirstOrDefault().Id, UserSMAId=ccv},
            new FeedComment{Content="Content3", CreatedDate=DateTime.Today, FeedId=context.Feeds.Where(s=>s.Libel=="Libel3").FirstOrDefault().Id, UserSMAId=ccv},
            
            };

            FeedComments.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.FeedComments.Add(s);
                context.SaveChanges();

            });


            var Notifications = new List<Notification>
            {
            new Notification{Url="notif1", Content="contentnotif1" , UserSMAId=ccv},
            new Notification{Url="notif2", Content="contentnotif2" , UserSMAId=ccv},
            new Notification{Url="notif3", Content="contentnotif3" , UserSMAId=ccv},
            new Notification{Url="notif4", Content="contentnotif4" , UserSMAId=ccv},

            };

            Notifications.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Notifications.Add(s);
                context.SaveChanges();

            });




            var StudentComments = new List<StudentComment>
            {
            new StudentComment{Content="Content1", CreatedDate=DateTime.Today, StudentId=context.Students.Where(c=>c.FirstName=="Student").FirstOrDefault().Id, UserSMAId=ccv},
            new StudentComment{Content="Content2", CreatedDate=DateTime.Today, StudentId=context.Students.Where(c=>c.FirstName=="sam").FirstOrDefault().Id, UserSMAId=ccv},

            };

            StudentComments.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.StudentComments.Add(s);
                context.SaveChanges();

            });



            var Testimonials = new List<Testimonial>
            {
            new Testimonial{Content="Content1", CreatedDate=DateTime.Today, ParentId=context.Parents.Where(i=>i.FirstName=="ouni").FirstOrDefault().Id, TeacherId=context.Teachers.Where(t=>t.FirstName=="mouna").First().Id},
            new Testimonial{Content="Content2", CreatedDate=DateTime.Today, ParentId=context.Parents.Where(i=>i.FirstName=="salloum").FirstOrDefault().Id, TeacherId=context.Teachers.Where(t=>t.FirstName=="haythem").First().Id},
            new Testimonial{Content="Content3", CreatedDate=DateTime.Today, ParentId=context.Parents.Where(i=>i.FirstName=="snoussi").FirstOrDefault().Id, TeacherId=context.Teachers.Where(t=>t.FirstName=="hela").First().Id},
            new Testimonial{Content="Content4", CreatedDate=DateTime.Today, ParentId=context.Parents.Where(i=>i.FirstName=="tawfik").FirstOrDefault().Id, TeacherId=context.Teachers.Where(t=>t.FirstName=="mohamed").First().Id},


            };

            Testimonials.ForEach(s =>
            {
                s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                context.Testimonials.Add(s);
                context.SaveChanges();

            });

            //ici commence l'erreur d'initialisation

            //var cc1 = context.Feeds.Where(s => s.UserSMAId == ccv).First().Id;
            //var Events = new List<Event>
            //{                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            //new Event{Begin=DateTime.Today,End=DateTime.Today, UserSMAId=ccv},
            ////new Event{Begin=DateTime.Today,End=DateTime.Today, UserSMAId=context.SchoolHeads.Where(s=>s.FirstName =="ouni").FirstOrDefault().Id},
            //};

            //Events.ForEach(s =>
            //{
            //    s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
            //    context.Events.Add(s);
            //    context.SaveChanges();

            //});





            //var Schools = new List<School>
            //{
            //new School{ },


            //};

            //Schools.ForEach(s =>
            //{
            //    s.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
            //    context.Schools.Add(s);
            //    context.SaveChanges();

            //});

            

        }




    }
}

