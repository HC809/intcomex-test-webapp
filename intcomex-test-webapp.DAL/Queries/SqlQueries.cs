using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intcomex_test_webapp.DAL.Queries
{
    public class SqlQueries
    {
        public static string DepartmentSalaries = @"select 
	                                                    dept.dept_name name,
                                                        sum(sal.salary) total
                                                    from departments dept
                                                    left join dept_emp dxe on dept.dept_no = dxe.dept_no
                                                    left join employees emp on dxe.emp_no = emp.emp_no
                                                    left join salaries sal on emp.emp_no = sal.emp_no
                                                    group by (dept.dept_name)
                                                    order by total desc;";

        public static string EmployeesTotalBorn = @"select 
	                                	            emp.emp_no as empno,
                                                    emp.first_name as firstname,
                                                    emp.last_name as lastname,
                                                    if(emp.gender='M', 'Masculino', 'Femenino') as gender,
                                                    emp.hire_date as hiredate,
                                                    emp.birth_date as birthdate,
                                                    (select count(emp_no) from employees where birth_date = emp.birth_date) AS same,
                                                    (select count(emp_no) from employees where birth_date = date_sub(emp.birth_date, interval 1 day)) AS earlier,
                                                    (select count(emp_no) from employees where birth_date = date_add(emp.birth_date, interval 1 day)) AS later
                                                from employees emp";
    }
}
