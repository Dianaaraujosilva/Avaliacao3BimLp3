using Microsoft.Data.Sqlite;
using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Avaliacao3BimLp3.Repositories;

var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);

var studentRepository = new StudentRepository(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Student")
{
    if(modelAction == "New")
    {
        var registration = args[2];
        var name = args[3];
        var city  = args[4];
        var former = false;

        var student = new Student(registration, name, city , former);
        
	if(studentRepository.ExistsById(registration))
        {
            Console.WriteLine($"Estudante com Registration {registration} já existe");
        }else
        {
            studentRepository.Save(student);
            Console.WriteLine($"Estudante {name} cadastrado com sucesso");
        }
    }

    if(modelAction == "Delete")
    {
        string registration = args[2];

        if(studentRepository.ExistsById(registration))
        {
            studentRepository.Delete(registration);
            Console.WriteLine($"Estudante {registration} removido com sucesso");
        }else
        {
            Console.WriteLine($"Estudante {registration} não encontrado!");
        }
    }

    if(modelAction == "MarkAsFormed")
    {
        string registration = args[2];

        if(studentRepository.ExistsById(registration))
        {
            studentRepository.MarkAsFormed(registration);
            Console.WriteLine($"Estudante {registration} definido como formado");
        }else
        {
            Console.WriteLine($"Estudante {registration} não encontrado!");
        }
    }

    if(modelAction == "List")
    {
        if(studentsRepository.GetAllFormed().Any())
        {
            Console.WriteLine("Student List");
            foreach (var student in students)
            {
		var former = student.Former ? "formado" : "não formado";
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, {student.Former}");
            } 
        }else
        {
            Console.WriteLine("Não encontrado");
        } 
    }

    if(modelAction == "ListFormed")
    {
        if(studentRepository.GetAllFormed().Any())
        {
            Console.WriteLine("Student List");
            foreach (var student in studentRepository.GetAllFormed())
            {
		var former = student.Former ? "formado" : "não formado";
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, {student.Former}");
            } 
        }else
        {
            Console.WriteLine("Não encontrado");
        } 
    }

    if(modelAction == "ListByCity")
    {
        var city = args[4];

        if(studentRepository.GetAllStudentByCity(city).Any())
        {
            Console.WriteLine("Student List");
            foreach (var student in studentRepository.GetAllStudentByCity(city))
            {
		var former = student.Former ? "formado" : "não formado";
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, {student.Former}");
            } 
        }else
        {
            Console.WriteLine("Não encontrado");
        } 
    }

    if(modelAction == "ListByCities")
    {
	var cities = new String[args.Length - 2];
        
        for(int i = 2; i < args.GetLength(1); i++)
        {
            cities[i-2] = args[i];
        }

        if(studentRepository.GetAllByCities(cities).Any())
        {
            Console.WriteLine("Student List");
            foreach (var student in studentRepository.GetAllByCities(cities))
            {
		var former = student.Former ? "formado" : "não formado";
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, {student.Former}");
            } 
        }else
        {
            Console.WriteLine("Não encontrado");
        } 

    }
    
     if(modelAction == "CountByCities")
     {
        List<CountStudentGroupByAttribute> results = studentRepository.CountByCities();
        if(results.Count() > 0)
        {
            foreach(var group in groups)
            {
                Console.WriteLine($"{result.AttributeName}, {result.StudentNumber}");
            }
        }

        else
        {
            Console.WriteLine("Nenhum estudante cadastrado");
        }
    }
    
        if(modelAction == "CountByFormed")
        {
	   List<CountStudentGroupByAttribute> results = studentRepository.CountByFormed();
           if(results.Count() > 0)
           {
                foreach(var group in groups)
                {
                    var former = group.AttributeName == "1" ? "Formados" : "Não formados";
                    Console.WriteLine($"{former}, {group.StudentNumber}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum estudante cadastrado");
            }
        }
    }
}
