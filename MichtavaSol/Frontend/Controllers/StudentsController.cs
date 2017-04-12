﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Entities.Models;
using FileHandler;
using Frontend.Areas.Administration.Models;
using SmartTextBox;
using Frontend.Models;
using Services.Interfaces;

namespace Frontend.Controllers
{
    public class StudentsController : Controller
    {
        private IFileManager _fileManager = new FileManager();
        private ISmartTextBox _smartTextBox = new SmartTextBoxImpl();
        private Policy _policy = new Policy();
        private Homework _homework = new Homework();
        private SmartTextViewModel smartView = new SmartTextViewModel();
        private Text text = new Text();

        private readonly ISubjectService _subjectServiceService;
        private readonly IHomeworkService _homeworkService;
        public StudentsController(ISubjectService subjectServiceService, IHomeworkService homeworkService)
        {
            _subjectServiceService = subjectServiceService;
            _homeworkService = homeworkService;
        }

        // GET: Students
        public ActionResult Subjects()
        {
            ViewBag.Title = "בחר נושא";

            IQueryable<SubjectsListViewModel> subjects =
                _subjectServiceService.All().Project().To<SubjectsListViewModel>(); //this uses a mapping for AutoMapper
            return View(subjects);
        }

        public ActionResult ChooseSubject()
        {
            ViewBag.Title = "בחר תת-נושא";

            return View("SubSubjects");
        }

        public ActionResult ChooseSubSubject()
        {
            ViewBag.Title = "בחר טקסט";

            return View("Texts");
        }

        public ActionResult ChooseText()
        {
            ViewBag.Title = "בחר פעולה";

            return View("TextMenu");
        }

        public ActionResult ChooseAction()
        {
            ViewBag.Title = "טקסט";

            //we want to pass the real text model, not only the string..
            //we get all the real data when start get it from the dal..
        //    text.Id = 10;
          //  text.Name = "המסמר";
            
/*
        public DateTime UploadTime { get; set; }

        public FileFormats Format { get; set; }

        public string FilePath { get; set; }
            */
            //should get the path from the text..
            TempData["TextContent"] = _fileManager.GetText(@"C:\Users\mweiss\Desktop\Test.txt");

            return View("TextView");
        }

        public ActionResult GotoSmartTextBox()
        {
            

            ViewBag.Title = "שאלות לתיבת טקסט חכמה";
            TempData["TextContent"] = _fileManager.GetText(@"C:\Users\mweiss\Desktop\Test.txt");
            TempData["QuestionContent"] = getQuestionSample().Content;


            if (TempData["NumberOfWords"] == null && TempData["NumberOfConnectorWords"] == null)
            {
                TempData["NumberOfWords"] = "0";
                TempData["NumberOfConnectorWords"] = "0";
                TempData["toManyWords"] = "";
            }

            InitializeSmartView();
            TempData["TextContent"] = _fileManager.GetText(@"C:\Users\mweiss\Desktop\Test.txt");

            
            
            return View("HomeWorkView", smartView);
        }

       

        


        public ActionResult AnalyzeAnswer()
        {
            ViewBag.Title = "שאלות לתיבת טקסט חכמה";
            TempData["TextContent"] = _fileManager.GetText(@"C:\Users\mweiss\Desktop\Test.txt");

            TempData["QuestionContent"] = getQuestionSample().Content;
            // here we have to call the SmartTextBox in server side

            string input = Request.Form["TextBoxArea"];
            int numOfWords = _smartTextBox.GetNumberOfWords(input);
            int numOfConnectors = _smartTextBox.GetNumberOfConnectors(input);
            TempData["NumberOfWords"] = numOfWords;
            TempData["NumberOfConnectorWords"] = numOfConnectors;
            TempData["Answer"] = input;

            //כשנוסיף את הפוליסי שתרוץ לא תהיה כנראה את הבעיה.. בינתיים
            InitializePolicy();

            if (numOfWords > _policy.MaxWords)
            {
                TempData["toManyWords"] = "הכנסת " + numOfWords + " מילים, אבל מותר לכל היותר " + _policy.MaxWords + " מילים.";
            }
            if (numOfConnectors > _policy.MaxConnectors)
            {
                TempData["toManyConnectors"] = "הכנסת " + numOfConnectors + " מילות קישור, אבל מותר לכל היותר " + _policy.MaxConnectors + " מילות קישור.";
            }
            InitializeSmartView();
            return View("HomeWorkView", smartView);
        }

        private void InitializePolicy()
        {
            _policy = new Policy();
            HashSet<string> _keySentencesList = new HashSet<string>();
            _keySentencesList.Add("התשובה לשאלה שנשאלה היא 1");
            _keySentencesList.Add("התשובה לשאלה שנשאלה היא 2");
            _keySentencesList.Add("התשובה לשאלה שנשאלה היא 3");
            _keySentencesList.Add("התשובה לשאלה שנשאלה היא 4");


            _policy = new Policy() { Id = "1", MinWords = 20, MaxWords = 30, MinConnectors = 3, MaxConnectors = 8, KeySentences = _keySentencesList };
        }

        private void InitializeSmartView()
        {


            smartView.question = getQuestionSample();
        

            // string text = _fileManager.GetText(@"C:\Users\mweiss\Desktop\Test.txt");
            // smartView.text = text;



            // IQueryable<SmartTextViewModel> home_works = _homeworkService.All().Project().To<SmartTextViewModel>();

            //smartView = home_works.GetEnumerator().First().To<SmartTextViewModel>();

        }

        public Question getQuestionSample()
        {
            InitializePolicy();

            Question q = new Question(); //local question init
            q.Id = 11;
            q.Content = "שאלה לדוגמא שנשלוף מהבסיס נתונים, מהו מיהו וכד'.. עוד כמה דברים.. ענה בנימוק.";
            q.Policy = _policy;
            HashSet<string> _keySentencesList = new HashSet<string>();
            _keySentencesList.Add("התשובה לשאלה שנשאלה היא 1");
            _keySentencesList.Add("התשובה לשאלה שנשאלה היא 2");
            _keySentencesList.Add("התשובה לשאלה שנשאלה היא 3");
            _keySentencesList.Add("התשובה לשאלה שנשאלה היא 4");

            q.Suggested_Openings = _keySentencesList;
            return q;
        }
    }
}