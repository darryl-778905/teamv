var QuestionsModel = function(questions) {
    var self = this;
    var count = 1;
    self.questions = ko.observableArray(  
        ko.utils.arrayMap(questions, function (question) {
            return {
            QuestionNumber: ko.observable(count), 
            Answers: question.Answers,
            Limits: question.Limits, 
            Mandatory: question.Mandatory,
            Question: question.Question, 
            Type: question.Type, 
            Options: ko.observableArray(question.Options),   
        };

    }));
 
    self.addYesNoQuestion = function () {
        self.questions.push({
            QuestionNumber: count++,
            Answers: "",
            Limits: 2, 
            Mandatory: "false", 
            Question: "", 
            Type: "YesNo", 
            Options: ko.observableArray()
        });
    };
    self.addFreeFormQuestion = function () {
        self.questions.push({
            QuestionNumber: count++,
            Answers: "",
            Limits: 250, 
            Mandatory: "false", 
            Question: "", 
            Type: "FreeForm", 
            Options: ko.observableArray()
        });
    };
    self.addMultiOptionQuestion = function () {
        self.questions.push({
            QuestionNumber: count++,
            Answers: "",
            Limits: 4,
            Mandatory: "false", 
            Question: "", 
            Type: "MultipleOption", 
            Options: ko.observableArray()
        });
    };

 
    self.removeQuestion = function (question) {
        self.questions.remove(question);
    };
 
    self.addOption = function (question) {
        question.Options.push({
            Option: ""
        })
    };
 
    self.removeOption = function(option) {
        $.each(self.questions(), function() { this.Options.remove(option) })
    };
 
    self.save = function() {
        self.lastSavedJson(JSON.stringify(ko.toJS(self.questions), null, 2));
    };
 
    self.lastSavedJson = ko.observable("")
};
 
ko.applyBindings(new QuestionsModel());
