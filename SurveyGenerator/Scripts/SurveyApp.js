var initialData = [
    { 
        Question: "",
        QuestionNumber: 0,
        Type: "YesNo",
        Mandatory: "false",
        Limits: 3,
        Options: []
    }
];
 
var QuestionsModel = function(questions) {
    var self = this;
    
    self.questions = ko.observableArray(
        ko.utils.arrayMap(questions, function(question) {
        return {
            Question: question.Question, 
            QuestionNumber: question.QuestionNumber, 
            Type: question.Type, 
            Mandatory: question.Mandatory, 
            Limits: question.Limits, 
            Options: ko.observableArray(question.Options)
        };

    }));
 
    self.addYesNoQuestion = function() {
        self.questions.push({
            Question: "",
            QuestionNumber: 0,
            Type: "YesNo",
            Mandatory: "false",
            Limits: 3,
            Options: ko.observableArray()
        });
    };
    self.addFreeFormQuestion = function () {
        self.questions.push({
            Question: "",
            QuestionNumber: 0,
            Type: "FreeForm",
            Mandatory: "false",
            Limits: 250,
            Options: ko.observableArray()
        });
    };
    self.addMultiOptionQuestion = function () {
        self.questions.push({
            Question: "",
            QuestionNumber: 0,
            Type: "MultipleOption",
            Mandatory: "false",
            Limits: 10,
            Options: ko.observableArray()
        });
    };

 
    self.removeQuestion = function(question) {
        self.questions.remove(question);
    };
 
    self.addOption = function(question) {
        question.Options.push({
            Option: ""
        });
    };
 
    self.removeOption = function(option) {
        $.each(self.questions(), function() { this.Options.remove(option) })
    };
 
    self.save = function() {
        self.lastSavedJson(JSON.stringify(ko.toJS(self.questions), null, 2));
    };
 
    self.lastSavedJson = ko.observable("")
};
 
ko.applyBindings(new QuestionsModel(initialData));
