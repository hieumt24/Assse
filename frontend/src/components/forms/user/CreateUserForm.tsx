import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { GENDERS, LOCATIONS, ROLES } from "@/constants";
import { removeExtraWhitespace } from "@/lib/utils";
import { createUserService } from "@/services";
import { createUserSchema } from "@/validations";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { z } from "zod";

export const CreateUserForm = () => {
  // Define form

  const form = useForm<z.infer<typeof createUserSchema>>({
    mode: "all",
    resolver: zodResolver(createUserSchema),
    defaultValues: {
      firstName: "",
      lastName: "",
      dateOfBirth: "",
      joinedDate: "",
      gender: "2",
      roleId: "2",
      location: "1",
    },
  });

  // Function handle onSubmit
  const onSubmit = async (values: z.infer<typeof createUserSchema>) => {
    const gender = parseInt(values.gender);
    const roleId = parseInt(values.roleId);
    const location = parseInt(values.location);
    const res = await createUserService({
      ...values,
      gender,
      roleId,
      location,
    });
    if (res.success) {
      toast.success(res.message);
    } else {
      toast.error(res.message);
    }
  };

  const navigate = useNavigate();

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="w-1/3 space-y-5 rounded-2xl bg-white p-6"
      >
        <h1 className="text-xl font-bold text-red-600">Create New User</h1>
        {/* First name */}
        <FormField
          control={form.control}
          name="firstName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>
                First Name <span className="text-red-600">*</span>
              </FormLabel>
              <FormControl>
                <Input
                  placeholder="Enter first name"
                  {...field}
                  onBlur={(e) => {
                    const cleanedValue = removeExtraWhitespace(e.target.value); // Clean the input value
                    field.onChange(cleanedValue); // Update the form state
                  }}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        {/* Last name */}
        <FormField
          control={form.control}
          name="lastName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>
                Last Name <span className="text-red-600">*</span>
              </FormLabel>
              <FormControl>
                <Input
                  placeholder="Enter last name"
                  {...field}
                  onBlur={(e) => {
                    const cleanedValue = removeExtraWhitespace(e.target.value); // Clean the input value
                    field.onChange(cleanedValue); // Update the form state
                  }}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        {/* Date of birth */}
        <FormField
          control={form.control}
          name="dateOfBirth"
          render={({ field }) => (
            <FormItem>
              <FormLabel>
                Date of birth <span className="text-red-600">*</span>
              </FormLabel>
              <FormControl>
                <Input {...field} type="date" className="justify-center" />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        {/* Joined date */}
        <FormField
          control={form.control}
          name="joinedDate"
          render={({ field }) => (
            <FormItem>
              <FormLabel>
                Joined Date <span className="text-red-600">*</span>
              </FormLabel>
              <FormControl>
                <Input {...field} type="date" className="justify-center" />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        {/* Gender */}
        <FormField
          control={form.control}
          name="gender"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Gender</FormLabel>
              <FormControl>
                <RadioGroup
                  onValueChange={field.onChange}
                  defaultValue={field.value}
                  className="flex gap-8"
                >
                  {GENDERS.map((gender) => {
                    return (
                      <FormItem
                        className="flex items-center space-x-3 space-y-0"
                        key={gender.value}
                      >
                        <FormControl>
                          <RadioGroupItem value={gender.value.toString()} />
                        </FormControl>
                        <FormLabel className="font-normal">
                          {gender.label}
                        </FormLabel>
                      </FormItem>
                    );
                  })}
                </RadioGroup>
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        {/* Role */}
        <FormField
          control={form.control}
          name="roleId"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Types</FormLabel>
              <FormControl>
                <Select value={field.value} onValueChange={field.onChange}>
                  <SelectTrigger>
                    <SelectValue placeholder="Role" />
                  </SelectTrigger>
                  <SelectContent>
                    {ROLES.map((role) => (
                      <SelectItem
                        key={role.value}
                        value={role.value.toString()}
                      >
                        {role.label}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        {/* Location */}
        <FormField
          control={form.control}
          name="location"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Location</FormLabel>
              <FormControl>
                <RadioGroup
                  onValueChange={field.onChange}
                  defaultValue={field.value}
                  className="flex gap-8"
                >
                  {LOCATIONS.map((location) => {
                    return (
                      <FormItem
                        className="flex items-center space-x-3 space-y-0"
                        key={location.value}
                      >
                        <FormControl>
                          <RadioGroupItem value={location.value.toString()} />
                        </FormControl>
                        <FormLabel className="font-normal">
                          {location.label}
                        </FormLabel>
                      </FormItem>
                    );
                  })}
                </RadioGroup>
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <div className="flex justify-end gap-4">
          <Button
            type="submit"
            className="w-[76px] bg-red-500 hover:bg-white hover:text-red-500"
            disabled={!form.formState.isValid}
          >
            Save
          </Button>
          <Button
            type="button"
            className="w-[76px] border bg-white text-black shadow-none hover:text-white"
            onClick={() => {
              navigate("/admin/user");
            }}
          >
            Cancel
          </Button>
        </div>
      </form>
    </Form>
  );
};
