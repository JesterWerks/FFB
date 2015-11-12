//@@ sourceURL=VM/AdminIndexVM.js

jQuery(document).ready(function () {
    BindViewModel();

});

var ScheduleType = function () {
    var self = this;

    self.Id = ko.observable();
    self.Name = ko.observable();
};

var Admin = function () {
    var self = this;

    self.ScheduleTypeList = ko.observableArray([]);
    self.SelectedScheduleType = ko.observable();
    self.SelectedYear = ko.observable(new Date().getFullYear());
    self.SelectedWeek = ko.observable();
    self.GetScheduleTypeList = function () {
        jQuery.getJSON("../api/AdminAPI/GetScheduleTypes", {}, function (data) {
            var scheduleTypes = [];
            jQuery.each(data, function () {
                var schedType = new ScheduleType();
                schedType.Id(this.Id);
                schedType.Name(this.Name);
                scheduleTypes.push(schedType);
            });
            self.ScheduleTypeList(scheduleTypes);
        });
    };
    self.GetScheduleTypeList();

    self.ImportPlayerList = function () {

    };
};


function BindViewModel() {

    // create instance of model
    var vm = new Admin();

    // setup validation for instance
    //vm.errors = ko.validation.group(vm, { deep: true });

    // apply bindings
    ko.applyBindings(vm);

};