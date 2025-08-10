
import '../../styles/login.css'
import { Link, useNavigate } from 'react-router-dom';
import { SubmitHandler, useForm } from 'react-hook-form';
import { AuthService } from '../services/auth.service';

type FormFields = {
    email: string;
    password: string;
    confirmPassword: string;
    role: number;
};

export default function Register() {
    const navigate = useNavigate();

    const {
        register,
        handleSubmit,
        formState: { errors, isSubmitting, isValid, isDirty },
    } = useForm<FormFields>({
        mode: 'onChange',
        defaultValues: {
            email: '',
            password: '',
            confirmPassword: '',
            role: 1
        },
    });

    const validatePassword = (value: string) => {
        if (value.length < 8) return 'Password must be at least 8 characters';
        if (!/[A-Z]/.test(value)) return 'Password must contain an uppercase letter';
        if (!/\d/.test(value)) return 'Password must contain a number';
        return true;
    };

    const onSubmit: SubmitHandler<FormFields> = async (data: FormFields) => {
        const apiResponse = await AuthService.register(data);
        if (apiResponse.success) {
            alert("Registered");
            navigate('/auth/login');
        } else {
            alert(apiResponse.message);
        }
    };

    return (
        <div className="login-container position-relative">
            <div className="loginbox bg-white position-relative pb-4">
                <div className="title position-relative text-center w-100 h-[35px] pt-2">SIGN Up</div>

                <form onSubmit={handleSubmit(onSubmit)}>
                    <div className="loginbox-textbox pt-3 px-4">
                        <label htmlFor="email">Email</label>
                        <input type="email"
                            className="form-control"
                            placeholder="Email"
                            id="email"
                            {...register('email', {
                                required: 'Email is required',
                                pattern: {
                                    value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                                    message: 'Invalid email address',
                                },
                            })}
                        />
                        {errors.email && <p className='text-danger mb-0'>{errors.email.message}</p>}
                    </div>
                    <div className="pt-3 px-4">
                        <label htmlFor="password">Password</label>
                        <input type="password"
                            className="form-control"
                            placeholder="Password"
                            id="password"

                            {...register('password', {
                                validate: validatePassword,
                            })}
                        />
                        {errors.password && <p className='text-danger mb-0'>{errors.password.message}</p>}
                    </div>

                    <div className="pt-3 px-4">
                        <label htmlFor="confirm-password">Confirm Password</label>
                        <input type="password"
                            className="form-control"
                            placeholder="Confirm Password"
                            id="confirm-password"
                            {...register('confirmPassword', {
                                required: 'Please confirm your password',
                                validate: (value, formFields) =>
                                    value === formFields.password || 'Passwords do not match'
                            })}
                        />
                        {errors.confirmPassword && <p className='text-danger mb-0'>{errors.confirmPassword.message}</p>}
                    </div>

                    <div className="pt-3 px-4">
                        <button type="submit" className="btn btn-primary btn-block w-100" disabled={isSubmitting || !isDirty}>
                            {isSubmitting ? 'Submitting...' : 'Register'}
                        </button>
                    </div>
                </form>

                <div className="text-center pt-2">
                    <Link className="sign-up-text" to="/auth/login">Sign in With Email</Link>
                </div>
            </div>
        </div>
    )
}
