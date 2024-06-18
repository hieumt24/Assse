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
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const createAssetSchema = z.object({
    name: z.string(),
    category: z.string(),
    specification: z.string(),
    installedDate: z.string(),
    state: z.enum(['Available', 'Not available'])
})
type CreateAssetFormData = z.infer<typeof createAssetSchema>;

export const CreateAssetForm: React.FC = () => {
    const form = useForm<CreateAssetFormData>({
        mode: "all",
        resolver: zodResolver(createAssetSchema),
    });

    const onSubmit = async (values: CreateAssetFormData) => {
        console.log(values);
        toast.success("Asset created successfully!");
    };

    return (
        <Form {...form}>
            <form
                onSubmit={form.handleSubmit(onSubmit)}
                className="h-[740px] w-1/3 space-y-5 rounded-2xl bg-white p-6 shadow-md"
            >
                <h1 className="text-2xl font-bold text-red-600">Create New Asset</h1>

                {/* Name */}
                <FormField
                    control={form.control}
                    name="name"
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel className="text-md">
                                Name <span className="text-red-600">*</span>
                            </FormLabel>
                            <FormControl>
                                <Input
                                    placeholder="Enter asset name"
                                    {...field}
                                    onBlur={(e) => {
                                        const cleanedValue = e.target.value.trim();
                                        field.onChange(cleanedValue);
                                    }}
                                    autoFocus
                                />
                            </FormControl>
                            <FormMessage>{form.formState.errors.name?.message}</FormMessage>
                        </FormItem>
                    )}
                />

                {/* Category */}
                <FormField
                    control={form.control}
                    name="category"
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel className="text-md">
                                Category <span className="text-red-600">*</span>
                            </FormLabel>
                            <FormControl>
                                <Select value={field.value} onValueChange={field.onChange}>
                                    <SelectTrigger>
                                        <SelectValue placeholder="Select category" />
                                    </SelectTrigger>
                                    <SelectContent>
                                        <SelectItem value="Category 1">Category 1</SelectItem>
                                        <SelectItem value="Category 2">Category 2</SelectItem>
                                        <SelectItem value="Category 3">Category 3</SelectItem>
                                    </SelectContent>
                                </Select>
                            </FormControl>
                            <FormMessage>{form.formState.errors.category?.message}</FormMessage>
                        </FormItem>
                    )}
                />

                {/* Specification */}
                <FormField
                    control={form.control}
                    name="specification"
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel className="text-md">
                                Specification <span className="text-red-600">*</span>
                            </FormLabel>
                            <FormControl>
                                <Input
                                    placeholder="Enter specification"
                                    {...field}
                                />
                            </FormControl>
                            <FormMessage>{form.formState.errors.specification?.message}</FormMessage>
                        </FormItem>
                    )}
                />

                {/* Installed Date */}
                <FormField
                    control={form.control}
                    name="installedDate"
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel className="text-md">
                                Installed Date <span className="text-red-600">*</span>
                            </FormLabel>
                            <FormControl>
                                <Input style={{ justifyContent: "center" }} type="date" {...field} />
                            </FormControl>
                            <FormMessage>{form.formState.errors.installedDate?.message}</FormMessage>
                        </FormItem>
                    )}
                />

                {/* State */}
                <FormField
                    control={form.control}
                    name="state"
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel className="text-md">State</FormLabel>
                            <FormControl>
                                <RadioGroup
                                    onValueChange={field.onChange}
                                    defaultValue={field.value}
                                    className="flex gap-8"
                                >
                                    <FormItem className="flex items-center space-x-3 space-y-0">
                                        <FormControl>
                                            <RadioGroupItem value="Available" />
                                        </FormControl>
                                        <FormLabel defaultValue={"true"} className="font-normal">Available</FormLabel>
                                        <FormControl>
                                            <RadioGroupItem value="Not available" />
                                        </FormControl>
                                        <FormLabel className="font-normal">Not available</FormLabel>
                                    </FormItem>
                                </RadioGroup>
                            </FormControl>
                            <FormMessage>{form.formState.errors.state?.message}</FormMessage>
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
                    >
                        Cancel
                    </Button>
                </div>
            </form>
        </Form>
    );
};
